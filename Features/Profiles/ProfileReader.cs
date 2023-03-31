using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;

namespace RealWorld.Features.Profiles;

public class ProfileReader : IProfileReader
{
    readonly RealWorldContext _context;
    readonly ICurrentUserAccessor _currentUserAccessor;
    public ProfileReader(RealWorldContext context, ICurrentUserAccessor currentUserAccessor)
    {
        _context = context;
        _currentUserAccessor = currentUserAccessor;
    }
    async Task<Profile> IProfileReader.GetProfile(string UserName)
    {
        var user = await _context.Users.Include(x => x.Followers)
            .FirstOrDefaultAsync(x => x.UserName == UserName);
        if (user == null)
        {
            return new Profile();
        }

        return new Profile()
        {
            Username = user.UserName,
            Bio = user.Bio,
            Image = user.Image,
            Following = user.Followers.Any(x => x.UserName == _currentUserAccessor.GetCurrentUsername()),
        };
    }
}
