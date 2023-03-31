using System.Net;
using Conduit.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Articles;

public class Create
{
    public record Article(string Title, string Description, string Body, ICollection<string> TagList);
    public record Command(Article Article) : IRequest<ArticleEnvelope> { }

    public class CommandHandler : IRequestHandler<Command, ArticleEnvelope>
    {
        readonly RealWorldContext _context;
        readonly ICurrentUserAccessor _currentUserAccessor;
        readonly ILogger<Create> _logger;
        readonly IArticleReader _articleReader;
        public CommandHandler(IArticleReader articleReader, RealWorldContext context, ICurrentUserAccessor currentUserAccessor, ILogger<Create> logger)
        {
            _currentUserAccessor = currentUserAccessor;
            _context = context;
            _logger = logger;
            _articleReader = articleReader;
        }


        public async Task<ArticleEnvelope> Handle(Command request, CancellationToken cancellationToken)
        {

            var author = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == (_currentUserAccessor.GetCurrentUsername() ?? ""));
            if (author == null)
            {

                throw new RestException(HttpStatusCode.BadRequest, "Invalid UserName");
            }
            var slug = request.Article.Title.GenerateSlug();
            if (await _context.Articles.AnyAsync(x => x.Slug == slug))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Slug = Constants.IN_USE });
            }

            var article = new Domain.Article()
            {
                Author = author,
                Body = request.Article.Body,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Description = request.Article.Description,
                Title = request.Article.Title,
                Slug = slug,

            };
            await _context.Articles.AddAsync(article);


            foreach (var item in request.Article.TagList)
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Name == item);
                if (tag == null)
                {
                    var newTag = new Domain.Tag { Name = item };
                    newTag.Articles.Add(article);
                    await _context.Tags.AddAsync(newTag);
                }
                else
                {
                    tag.Articles.Add(article);
                }
            }

            await _context.SaveChangesAsync();
            return await _articleReader.GetArticle(slug);
        }
    }
}
