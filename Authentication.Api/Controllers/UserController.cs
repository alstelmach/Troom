using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Contracts.User.Commands;
using Core.Application.Abstractions.Messaging.Commands;
using Core.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Mvc;
using Controller = Core.Infrastructure.Mvc.Controller;

namespace Authentication.Api.Controllers
{
    [ApiController]
    [ApiVersion(DefaultApiVersion)]
    [Route(RoutePattern)]
    public sealed class UserController : Controller
    {
        public UserController(ICommandBus commandBus,
            IQueryBus queryBus)
                : base(commandBus, queryBus)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            command.ClaimsPrincipal = User;
            await CommandBus.SendAsync(command, cancellationToken);

            return Ok();
        }
    }
}
