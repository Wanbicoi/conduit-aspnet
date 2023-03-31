namespace RealWorld.Infrastructure.Security;
public interface IJwtTokenGenerator
{
    string CreateToken(string username);
}
