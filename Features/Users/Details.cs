using AutoMapper;
using MediatR;
using RealWorld.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace RealWorld.Features.Users;

public class Details
{
    public record Query(string UserName) : IRequest<UserEnvelope>;
    public class QueryHandler : IRequestHandler<Query, UserEnvelope>
    {
        RealWorldContext _context;
        IMapper _mapper;
        public QueryHandler(RealWorldContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserEnvelope> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            var u = _mapper.Map<Domain.User, User>(user!);
            return new UserEnvelope(new User());
        }
    }
}
