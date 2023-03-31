using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Articles;

public class UnFavorite
{
    public record Command(string Slug) : IRequest<ArticleEnvelope> { }

    public class CommandHandler : IRequestHandler<Command, ArticleEnvelope>
    {
        readonly RealWorldContext _context;
        readonly ICurrentUserAccessor _currentUserAccessor;
        readonly IArticleReader _articleReader;
        public CommandHandler(IArticleReader articleReader, RealWorldContext context, ICurrentUserAccessor currentUserAccessor)
        {
            _currentUserAccessor = currentUserAccessor;
            _context = context;
            _articleReader = articleReader;
        }

        public async Task<ArticleEnvelope> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.Include(x => x.FavoriteArticles)
                .FirstOrDefaultAsync(x => x.UserName == (_currentUserAccessor.GetCurrentUsername() ?? ""));
            if (currentUser == null)
                throw new RestException(HttpStatusCode.Unauthorized, "Unauthorized");

            var article = await _context.Articles.FirstOrDefaultAsync(x => x.Slug == request.Slug);
            if (article == null)
                throw new RestException(HttpStatusCode.UnprocessableEntity, new { Slug = Constants.NOT_FOUND });

            article.FavoriteUsers.Remove(currentUser);

            await _context.SaveChangesAsync();
            return await _articleReader.GetArticle(request.Slug);
        }
    }
}
