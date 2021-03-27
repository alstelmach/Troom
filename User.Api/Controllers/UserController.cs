using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Commands;
using Core.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Mvc;
using User.Application.Contracts.User.Commands;
using Controller = Core.Infrastructure.Mvc.Controller;

namespace User.Api.Controllers
{
    [ApiController]
    [ApiVersion(DefaultApiVersion)]
    [Route(RoutePattern)]
    public sealed class UserController : Controller
    {
        private const string ResourceName = "user";
        
        public UserController(ICommandBus commandBus,
            IQueryBus queryBus)
                : base(commandBus, queryBus)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = Guid.NewGuid();
            command.ClaimsPrincipal = User;
            
            await CommandBus.SendAsync(command, cancellationToken);

            var uri = GetUri(command.Id, ResourceName);
            var resultAction = Created(uri, null);

            return resultAction;
        }
    }
}
