using AutoMapper;
using MediatR;
using RealWorld.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure.Security;
using FluentValidation;
using System.Net;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Users;

public class Create
{
    public class CommandDTO
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class CommandDTOValidator : AbstractValidator<CommandDTO>
    {
        public CommandDTOValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
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
            if (await _context.Users.Where(x => x.UserName == request.User.UserName)
                    .AnyAsync(cancellationToken))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Username = Constants.IN_USE });
            }

            if (await _context.Users.Where(x => x.Email == request.User.Email)
                    .AnyAsync(cancellationToken))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Email = Constants.IN_USE });
            }

            var salt = Guid.NewGuid().ToByteArray();
            var user = new Domain.User()
            {
                UserName = request.User.UserName,
                Email = request.User.Email,
                Hash = _passwordHasher.Hash(request.User.Password, salt),
                Salt = salt,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var u = _mapper.Map<Domain.User, User>(user);
            u.Token = _jwtTokenGenerator.CreateToken(user.UserName);
            return new UserEnvelope(u);
        }
    }
}
