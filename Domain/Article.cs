
namespace RealWorld.Domain;

public class Article
{
    public int Id { get; set; }
    public string Slug { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Body { get; set; } = default!;

    public ICollection<Tag> Tags { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Favorited { get; set; }
    public int FavoritesCount => FavoriteUsers.Count;

    public User Author { get; set; } = default!;

    public ICollection<User> FavoriteUsers { get; set; }


    public Article() { FavoriteUsers = new List<User>(); }
}
