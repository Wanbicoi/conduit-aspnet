namespace RealWorld.Features.Profiles;
public class Profile
{
    public string Username { get; set; } = default!;
    public string Bio { get; set; } = default!;
    public string Image { get; set; } = default!;
    public bool Following { get; set; }
}
public record ProfileEnvelope(Profile Profile);
