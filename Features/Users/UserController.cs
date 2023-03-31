using RealWorld.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RealWorld.Infrastructure.Security;

namespace RealWorld.Features.Users
{
    [Route("user")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public class UserController
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UserController(IMediator mediator, ICurrentUserAccessor currentUserAccessor)
        {
            _mediator = mediator;
            _currentUserAccessor = currentUserAccessor;
        }

        /* [HttpGet("getCurrent")] */
        /* public Task<UserEnvelope> GetCurrent(CancellationToken cancellationToken) */
        /* { */
        /*     return _mediator.Send( */
        /*             new Details.Query( */
        /*                 _currentUserAccessor.GetCurrentUsername() ?? "<unknown>"), */
        /*                 cancellationToken */
        /*             ); */
        /* } */

        [HttpPut]
        public Task<UserEnvelope> UpdateUser([FromBody] Edit.Command command)
            => _mediator.Send(command);
    }
}
