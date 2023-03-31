using MediatR;
using Microsoft.EntityFrameworkCore;
using RealWorld.Features.Profiles;
using RealWorld.Infrastructure;

namespace RealWorld.Features.Articles;

public class List
{
    public record Query(string Tag, string Author, string Favorited, int Limit = 20, int Offset = 0) : IRequest<ArticlesEnvelope> { }

    public class QueryHandler : IRequestHandler<Query, ArticlesEnvelope>
    {
        readonly RealWorldContext _context;
        ILogger<QueryHandler> _logger;
        readonly ICurrentUserAccessor _currentUserAccessor;
        readonly IProfileReader _profileReader;
        readonly IArticleReader _articleReader;

        public QueryHandler(IArticleReader articleReader, RealWorldContext context, ILogger<QueryHandler> logger, IProfileReader profileReader, ICurrentUserAccessor currentUserAccessor)
        {
            _context = context;
            _logger = logger;
            _profileReader = profileReader;
            _currentUserAccessor = currentUserAccessor;
            _articleReader = articleReader;
        }

        public async Task<ArticlesEnvelope> Handle(Query request, CancellationToken cancellationToken)
        {
            var queryable = _context.Articles.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Tag))
            {
                queryable = queryable.Include(x => x.Tags).Where(x => x.Tags.Select(x => x.Name).Contains(request.Tag));
            }
            if (!string.IsNullOrWhiteSpace(request.Author))
            {
                queryable = queryable.Include(x => x.Author).Where(x => x.Author.UserName == request.Author);
            }
            if (!string.IsNullOrWhiteSpace(request.Favorited))
            {
                queryable = queryable.Include(x => x.FavoriteUsers).Where(x => x.FavoriteUsers.Select(x => x.UserName).Contains(request.Favorited));
            }
            var articleSlugs = await queryable
                .OrderByDescending(x => x.CreatedAt)
                .Skip(request.Offset)
                .Take(request.Limit)
                .Select(x => x.Slug)
                .ToListAsync(cancellationToken);
            var articlesDTO = new List<ArticleEnvelope>();
            foreach (var slug in articleSlugs)
            {
                articlesDTO.Add(await _articleReader.GetArticle(slug));
            }
            return new ArticlesEnvelope(articlesDTO, articlesDTO.Count);
        }
    }
}
