namespace RealWorld.Features.Profiles;

public interface IProfileReader
{
    Task<Profile> GetProfile(string UserName);
}
