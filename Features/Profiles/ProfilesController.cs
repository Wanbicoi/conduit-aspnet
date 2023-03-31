using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorld.Infrastructure.Security;

namespace RealWorld.Features.Profiles;

[Route("profiles")]
public class TagsController
{
    private readonly IMediator _mediator;
    public TagsController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("{username}")]
    public Task<ProfileEnvelope> Detail(Detail.Query query, CancellationToken cancellationToken)
        => _mediator.Send(query, cancellationToken);

    [HttpGet("{username}/follow")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public Task<ProfileEnvelope> ToggleFollow(ToggleFollow.Command command, CancellationToken cancellationToken)
        => _mediator.Send(command, cancellationToken);
}
