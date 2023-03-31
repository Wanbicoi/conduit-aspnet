
namespace RealWorld.Infrastructure.Security;

public interface IPasswordHasher : IDisposable
{
    byte[] Hash(string password, byte[] salt);
}
