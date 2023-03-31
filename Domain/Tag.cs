namespace RealWorld.Domain;

public class Tag
{
    public string Name { get; set; } = default!;

    public ICollection<Article> Articles { get; set; } = default!;

    public Tag() { Articles = new List<Article>(); }
}
