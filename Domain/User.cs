namespace RealWorld.Domain;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Bio { get; set; } = "";
    public string Image { get; set; } = "";

    public byte[] Hash { get; set; } = default!;
    public byte[] Salt { get; set; } = default!;

    public ICollection<Article> FavoriteArticles { get; set; } = default!;
    public ICollection<Article> CreatedArticles { get; set; } = default!;

    public ICollection<User> Followings { get; set; } = default!;
    public ICollection<User> Followers { get; set; } = default!;

    public User()
    {
        FavoriteArticles = new List<Article>();
        CreatedArticles = new List<Article>();

        Followings = new List<User>();
        Followers = new List<User>();
    }
}
