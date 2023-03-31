using MediatR;

namespace RealWorld.Features.Profiles;

public class Detail
{
    public record Query(string UserName) : IRequest<ProfileEnvelope> { }

    public class QueryHandler : IRequestHandler<Query, ProfileEnvelope>
    {
        readonly IProfileReader _profileReader;

        public QueryHandler(IProfileReader profileReader)
            => _profileReader = profileReader;

        public async Task<ProfileEnvelope> Handle(Query request, CancellationToken cancellationToken)
            => new ProfileEnvelope(await _profileReader.GetProfile(request.UserName));
    }
}
