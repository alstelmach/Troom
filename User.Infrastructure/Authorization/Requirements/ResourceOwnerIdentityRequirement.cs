using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
#pragma warning disable 1998

namespace User.Infrastructure.Authorization.Requirements
{
    public sealed class ResourceOwnerIdentityRequirement : AuthorizationHandler<ResourceOwnerIdentityRequirement>,
        IAuthorizationRequirement
    {
        private const string UserIdRouteKey = "userId";
        
        private readonly IServiceProvider _serviceProvider;

        public ResourceOwnerIdentityRequirement(IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ResourceOwnerIdentityRequirement requirement)
        {
            var isOwner = CheckIsOwner(context);

            if (isOwner)
            {
                context.Succeed(this);
            }
            else
            {
                context.Fail();
            }
        }

        private bool CheckIsOwner(AuthorizationHandlerContext context)
        {
            var isOwner = false;
            var isTokenProvided = context.User.Claims.Any();
            
            if (isTokenProvided)
            {
                var ownerId = context.User.GetOwnerId();
                var userId = GetUserIdFromRoute();
                isOwner = ownerId.Equals(userId);
            }

            return isOwner;
        }

        private Guid GetUserIdFromRoute()
        {
            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;
            var userIdString = httpContext!.Request.RouteValues[UserIdRouteKey]?.ToString() ?? string.Empty;
            var userId = Guid.Parse(userIdString);

            return userId;
        }
    }
}
