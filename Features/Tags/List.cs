using MediatR;
using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;

namespace RealWorld.Features.Tags;

public class List
{
    public class Query : IRequest<TagsEnvelope> { }

    public class QueryHandler : IRequestHandler<Query, TagsEnvelope>
    {
        readonly RealWorldContext _context;

        public QueryHandler(RealWorldContext context) => _context = context;

        public async Task<TagsEnvelope> Handle(Query request, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.Select(x => x.Name)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return new TagsEnvelope() { Tags = tags };
        }
    }
}
