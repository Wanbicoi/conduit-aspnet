using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealWorld.Features.Users
{
    [Route("users")]
    public class UsersController
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost("login")]
        public Task<UserEnvelope> Login([FromBody] Login.Command command)
            => _mediator.Send(command);

        [HttpPost()]
        public Task<UserEnvelope> Create([FromBody] Create.Command command)
            => _mediator.Send(command);
    }
}
