
namespace RealWorld.Features.Articles;

using RealWorld.Features.Profiles;

public record ArticleEnvelope(
    string Slug,
    string Title,
    string Description,
    string Body,
    ICollection<string> Tags,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    int FavoritesCount,
    Profile Author,
    bool Favorited
);
