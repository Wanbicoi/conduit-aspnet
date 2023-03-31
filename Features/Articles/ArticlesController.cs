using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorld.Infrastructure.Security;

namespace RealWorld.Features.Articles;

[Route("articles")]
public class ArticlesController
{
    private readonly IMediator _mediator;
    public ArticlesController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public Task<ArticlesEnvelope> GetAll([FromQuery] List.Query query, CancellationToken cancellationToken)
        => _mediator.Send(query, cancellationToken);


    [HttpGet("{slug}")]
    public Task<ArticleEnvelope> Get(string slug, CancellationToken cancellationToken)
        => _mediator.Send(new Detail.Query(slug), cancellationToken);

    [HttpPut("{slug}")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public Task<ArticleEnvelope> Edit([FromRoute] string Slug, [FromBody] Edit.Command command, CancellationToken cancellationToken)
    {
        command.Slug = Slug;
        return _mediator.Send(command, cancellationToken);
    }

    [HttpDelete("{slug}")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public Task Delete(Delete.Command command, CancellationToken cancellationToken)
        => _mediator.Send(command, cancellationToken);

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public Task<ArticleEnvelope> Create([FromBody] Create.Command command, CancellationToken cancellationToken)
        => _mediator.Send(command, cancellationToken);

    [HttpPost("{slug}/favorite")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public Task<ArticleEnvelope> Favorite(Favorite.Command command, CancellationToken cancellationToken)
        => _mediator.Send(command, cancellationToken);

    [HttpDelete("{slug}/favorite")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public Task<ArticleEnvelope> UnFavorite(UnFavorite.Command command, CancellationToken cancellationToken)
        => _mediator.Send(command, cancellationToken);
}
