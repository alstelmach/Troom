using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Commands;
using Core.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Mvc;
using User.Application.Contracts.Authentication;
using User.Application.Dto;
using Controller = Core.Infrastructure.Mvc.Controller;

namespace User.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    [ApiVersion(DefaultApiVersion)]
    public sealed class AuthenticationController : Controller
    {
        public AuthenticationController(ICommandBus commandBus,
            IQueryBus queryBus)
                : base(commandBus,
                    queryBus)
        {
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResultDto>> AuthenticateAsync([FromBody] AuthenticationQuery query,
            CancellationToken cancellationToken)
        {
            var authenticationResult = await QueryBus.QueryAsync(query, cancellationToken);
            
            return authenticationResult.IsAuthenticated
                ? Ok(authenticationResult)
                : Unauthorized(authenticationResult);
        }
    }
}
