namespace RealWorld.Infrastructure;

public interface ICurrentUserAccessor
{
    public string? GetCurrentUsername();
}
