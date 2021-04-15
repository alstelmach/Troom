using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Commands;
using Core.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Contracts.Authorization;
using User.Application.Dto;
using Controller = Core.Infrastructure.Mvc.Controller;

namespace User.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    [ApiVersion(DefaultApiVersion)]
    public sealed class AuthorizationController : Controller
    {
        public AuthorizationController(ICommandBus commandBus,
            IQueryBus queryBus)
                : base(commandBus,
                    queryBus)
        {
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AuthorizationResultDto>> AuthorizeAsync([FromQuery] Guid permissionId,
            CancellationToken cancellationToken)
        {
            var query = new AuthorizeQuery
            {
                PermissionId = permissionId,
                ClaimsPrincipal = User
            };

            var result = await QueryBus.QueryAsync(query, cancellationToken);

            return Ok(result);
        }
    }
}
