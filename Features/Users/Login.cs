using AutoMapper;
using MediatR;
using RealWorld.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure.Security;
using FluentValidation;
using System.Net;

namespace RealWorld.Features.Users;

public class Login
{
    public class CommandDTO
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public record Command(CommandDTO User) : IRequest<UserEnvelope>;
    public class CommandDTOValidator : AbstractValidator<CommandDTO>
    {
        public CommandDTOValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
    public class QueryHandler : IRequestHandler<Command, UserEnvelope>
    {
        readonly RealWorldContext _context;
        readonly IMapper _mapper;
        readonly IPasswordHasher _passwordHasher;
        readonly IJwtTokenGenerator _jwtTokenGenerator;
        public QueryHandler(RealWorldContext context, IMapper mapper, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<UserEnvelope> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Email == request.User.Email)
                .SingleOrDefaultAsync(cancellationToken);
            if (user == null || !user!.Hash.SequenceEqual(_passwordHasher.Hash(request.User.Password, user.Salt)))
            {
                throw new RestException(HttpStatusCode.UnprocessableEntity,
                        new { Error = "Invalid email / password." });
            }

            var u = _mapper.Map<Domain.User, User>(user);
            u.Token = _jwtTokenGenerator.CreateToken(user.UserName);
            return new UserEnvelope(u);
        }
    }
}
