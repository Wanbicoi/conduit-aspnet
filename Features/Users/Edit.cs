using AutoMapper;
using MediatR;
using RealWorld.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure.Security;
using FluentValidation;
using System.Net;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Users;

public class Edit
{
    public class CommandDTO
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Bio { get; set; } = "";
        public string Image { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public class CommandDTOValidator : AbstractValidator<CommandDTO>
    {
        public CommandDTOValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.Bio).NotNull().NotEmpty();
            RuleFor(x => x.UserName).NotNull().NotEmpty();
        }
    }
    public record Command(CommandDTO User) : IRequest<UserEnvelope>;
    public class QueryHandler : IRequestHandler<Command, UserEnvelope>
    {
        readonly RealWorldContext _context;
        readonly IMapper _mapper;
        readonly IJwtTokenGenerator _jwtTokenGenerator;
        readonly IPasswordHasher _passwordHasher;

        public QueryHandler(IPasswordHasher passwordHasher, RealWorldContext context, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserEnvelope> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Email == request.User.Email).SingleOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Email = Constants.NOT_FOUND });
            }
            if (!string.IsNullOrWhiteSpace(request.User.Password))
            {
                user.Salt = Guid.NewGuid().ToByteArray();
                user.Hash = _passwordHasher.Hash(request.User.Password, user.Salt);
            }
            user.UserName = request.User.UserName ?? user.UserName;
            user.Email = request.User.Email ?? user.Email;
            user.Bio = request.User.Bio ?? user.Bio;
            user.Image = request.User.Image ?? user.Image;

            await _context.SaveChangesAsync();

            var u = _mapper.Map<Domain.User, User>(user);
            u.Token = _jwtTokenGenerator.CreateToken(user.UserName);
            return new UserEnvelope(u);
        }
    }
}
