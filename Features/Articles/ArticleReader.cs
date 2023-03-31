using System.Net;
using Microsoft.EntityFrameworkCore;
using RealWorld.Features.Profiles;
using RealWorld.Infrastructure;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Articles;

public class ArticleReader : IArticleReader
{
    readonly RealWorldContext _context;
    readonly ICurrentUserAccessor _currentUserAccessor;
    readonly IProfileReader _profileReader;
    public ArticleReader(RealWorldContext context, ICurrentUserAccessor currentUserAccessor, IProfileReader profileReader)
    {
        _currentUserAccessor = currentUserAccessor;
        _context = context;
        _profileReader = profileReader;
    }
    async Task<ArticleEnvelope> IArticleReader.GetArticle(string Slug)
    {
        var article = await _context.Articles.Include(x => x.Tags).Include(x => x.Author).Include(x => x.FavoriteUsers)
            .FirstOrDefaultAsync(x => x.Slug == Slug);
        if (article == null)
        {
            throw new RestException(HttpStatusCode.UnprocessableEntity, new { Slug = Constants.NOT_FOUND });
        }
        return new ArticleEnvelope(
            article.Slug,
            article.Title,
            article.Description,
            article.Body,
            article.Tags.Select(x => x.Name).ToList(),
            article.CreatedAt,
            article.UpdatedAt,
            article.FavoritesCount,
            await _profileReader.GetProfile(article.Author.UserName),
            article.FavoriteUsers.Any(x => x.UserName == _currentUserAccessor.GetCurrentUsername())
        );
    }
}
