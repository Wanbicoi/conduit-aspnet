using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Articles;

public class Edit
{
    public record Article(string Title, string Description, string Body);
    public class Command : IRequest<ArticleEnvelope>
    {
        public Article Article { get; set; } = default!;
        public string Slug { get; set; } = default!;
    }

    public class CommandHandler : IRequestHandler<Command, ArticleEnvelope>
    {
        readonly RealWorldContext _context;
        readonly IArticleReader _articleReader;
        public CommandHandler(IArticleReader articleReader, RealWorldContext context)
        {
            _context = context;
            _articleReader = articleReader;
        }

        public async Task<ArticleEnvelope> Handle(Command request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(x => x.Slug == request.Slug);
            if (article == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Slug = Constants.NOT_FOUND });
            }
            if (request.Article.Body != null)
                article.Body = request.Article.Body;
            if (request.Article.Description != null)
                article.Description = request.Article.Description;
            if (request.Article.Title != null)
                article.Title = request.Article.Title;
            article.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await _articleReader.GetArticle(request.Slug);
        }
    }
}
