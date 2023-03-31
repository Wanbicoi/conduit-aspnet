using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealWorld.Features.Tags
{
    [Route("tags")]
    public class TagsController
    {
        private readonly IMediator _mediator;
        public TagsController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public Task<TagsEnvelope> List(CancellationToken cancellationToken)
            => _mediator.Send(new List.Query(), cancellationToken);
    }
}
