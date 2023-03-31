namespace RealWorld.Features.Articles;

public interface IArticleReader
{
    Task<ArticleEnvelope> GetArticle(string Slug);
}
