using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Profiles;

public class ToggleFollow
{
    public record Command(string UserName) : IRequest<ProfileEnvelope> { }

    public class CommandHandler : IRequestHandler<Command, ProfileEnvelope>
    {
        readonly RealWorldContext _context;
        readonly ICurrentUserAccessor _currentUserAccessor;
        public CommandHandler(RealWorldContext context, ICurrentUserAccessor currentUserAccessor)
        {
            _currentUserAccessor = currentUserAccessor;
            _context = context;
        }

        public async Task<ProfileEnvelope> Handle(Command request, CancellationToken cancellationToken)
        {
            var following = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            if (following == null)
                throw new RestException(HttpStatusCode.UnprocessableEntity, new { UserName = Constants.NOT_FOUND });

            var user = await _context.Users.Include(x => x.Followings)
                .FirstOrDefaultAsync(x => x.UserName == _currentUserAccessor.GetCurrentUsername(), cancellationToken);

            bool isFollowing = false;
            if (user != null)
            {
                if (user.Followings.Any(x => x.UserName == request.UserName))
                {
                    user.Followings.Add(following);
                    isFollowing = true;
                }
                else
                {
                    user.Followings.Remove(following);
                    isFollowing = false;
                }
            }

            return new ProfileEnvelope(new Profile()
            {
                Username = following.UserName,
                Image = following.Image,
                Bio = following.Bio,
                Following = isFollowing,
            });
        }
    }
}
