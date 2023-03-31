using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;
using RealWorld.Infrastructure.Error;

namespace RealWorld.Features.Articles;

public class Delete
{
    public record Command(string Slug) : IRequest { }

    public class CommandHandler : IRequestHandler<Command>
    {
        readonly RealWorldContext _context;

        public CommandHandler(RealWorldContext context, ICurrentUserAccessor currentUserAccessor, ILogger<Create> logger)
            => _context = context;

        async Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(x => x.Slug == request.Slug, cancellationToken);

            if (article == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Article = Constants.NOT_FOUND });
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
