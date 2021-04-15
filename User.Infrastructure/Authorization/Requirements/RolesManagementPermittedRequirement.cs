using System;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Contracts.Authorization;

namespace User.Infrastructure.Authorization.Requirements
{
    public sealed class RolesManagementPermittedRequirement : AuthorizationHandler<RolesManagementPermittedRequirement>,
        IAuthorizationRequirement
    {
        private readonly IServiceProvider _serviceProvider;

        public RolesManagementPermittedRequirement(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RolesManagementPermittedRequirement requirement)
        {
            using var scope = _serviceProvider.CreateScope();
            var queryBus = scope.ServiceProvider.GetRequiredService<IQueryBus>();
            var authorizationQuery = new AuthorizeQuery
            {
                PermissionId = Guid.Parse(AuthorizationConstants.RoleManagementPermissionId),
                ClaimsPrincipal = context.User
            };

            var authorizationResultDto = await queryBus.QueryAsync(authorizationQuery);

            if (authorizationResultDto.IsAuthorized)
            {
                context.Succeed(this);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
