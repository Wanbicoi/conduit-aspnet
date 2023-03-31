using MediatR;

namespace RealWorld.Features.Articles;

public class Detail
{
    public record Query(string Slug) : IRequest<ArticleEnvelope> { }

    public class QueryHandler : IRequestHandler<Query, ArticleEnvelope>
    {
        readonly IArticleReader _articleReader;
        public QueryHandler(IArticleReader articleReader)
        {
            _articleReader = articleReader;
        }

        public async Task<ArticleEnvelope> Handle(Query request, CancellationToken cancellationToken)
            => await _articleReader.GetArticle(request.Slug);
    }
}
