using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Commands;
using Core.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Contracts.User.Commands;
using User.Application.Contracts.User.Queries;
using User.Application.Dto;
using User.Infrastructure.Authorization;
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
        [AllowAnonymous]
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

        [HttpPost("{userId}/roles")]
        [Authorize(Policy = AuthorizationPolicies.AdministrativePrivilegesRequiredPolicy)]
        public async Task<IActionResult> AssignUserRoleAsync([FromRoute] Guid userId,
            [FromBody] AssignRoleToUserCommand command,
            CancellationToken cancellationToken)
        {
            command.UserId = userId;
            command.ClaimsPrincipal = User;

            await CommandBus.SendAsync(command, cancellationToken);

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = AuthorizationPolicies.AdministrativePrivilegesRequiredPolicy)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var query = new GetUsersQuery
            {
                ClaimsPrincipal = User
            };

            var users = await QueryBus.QueryAsync(query, cancellationToken);

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [Authorize(Policy = AuthorizationPolicies.ResourceOwnerIdentityConfirmationRequiredPolicy)]
        public async Task<ActionResult<UserDto>> GetUserAsync([FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            var query = new GetUserQuery
            {
                Id = userId,
                ClaimsPrincipal = User
            };

            var user = await QueryBus.QueryAsync(query, cancellationToken);

            return user is not null
                ? Ok(user)
                : NotFound();
        }

        [HttpGet("{userId}/roles")]
        [Authorize(Policy = AuthorizationPolicies.AdministrativePrivilegesRequiredPolicy)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetUserRolesAsync([FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            var query = new GetUserRolesQuery
            {
                UserId = userId,
                ClaimsPrincipal = User
            };

            var roles = await QueryBus.QueryAsync(query, cancellationToken);

            return Ok(roles);
        }

        [HttpPut("{userId}/password")]
        [Authorize(Policy = AuthorizationPolicies.ResourceOwnerIdentityConfirmationRequiredPolicy)]
        public async Task<IActionResult> ChangeUserPasswordAsync([FromBody] ChangeUserPasswordCommand command,
            CancellationToken cancellationToken)
        {
            command.ClaimsPrincipal = User;

            await CommandBus.SendAsync(command, cancellationToken);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [Authorize(Policy = AuthorizationPolicies.ResourceOwnerIdentityConfirmationRequiredPolicy)]
        public async Task<IActionResult> DeleteUserAsync(CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand
            {
                ClaimsPrincipal = User
            };

            await CommandBus.SendAsync(command, cancellationToken);

            return Ok();
        }

        [HttpDelete("{userId}/roles")]
        [Authorize(Policy = AuthorizationPolicies.AdministrativePrivilegesRequiredPolicy)]
        public async Task<IActionResult> DenyUserRoleAsync([FromRoute] Guid userId,
            [FromBody] DenyUserRoleCommand command,
            CancellationToken cancellationToken)
        {
            command.UserId = userId;
            command.ClaimsPrincipal = User;

            await CommandBus.SendAsync(command, cancellationToken);

            return Ok();
        }
    }
}
