using System;
using System.Text;
using Core.Infrastructure.Identity;
using User.Infrastructure.Messaging;
using User.Infrastructure.Persistence;
using User.Infrastructure.Services;
using Core.Infrastructure.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using User.Infrastructure.Authorization;
using User.Infrastructure.Authorization.Requirements;
using User.Infrastructure.Persistence.Read;
using User.Infrastructure.Services.TokenGeneration;

namespace User.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        private const string TokenSectionKey = "TokenSettings";
        private const string SelfCheckName = "Self-check";
        private const int MajorVersion = 1;
        private const int MinorVersion = 0;

        public static IServiceCollection RegisterInfrastructureDependencies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration) =>
                serviceCollection
                    .AddHealthChecks()
                    .AddCheck(SelfCheckName, () => HealthCheckResult.Healthy())
                    .Services
                    .RegisterMessagingDependencies(configuration)
                    .RegisterPersistenceDependencies(configuration)
                    .AddMvcDependencies(MajorVersion, MinorVersion)
                    .RegisterInfrastructureServices(configuration)
                    .AddJwtServices(configuration)
                    .AddAuthorizationServices();

        public static IApplicationBuilder UseInfrastructureMiddlewares(this IApplicationBuilder builder,
            IServiceProvider serviceProvider) =>
                builder
                    .UseReadModelMiddlewares()
                    .Seed(serviceProvider);

        private static IServiceCollection AddJwtServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var tokenSettings = configuration
                .GetSection(TokenSectionKey)
                .Get<TokenSettings>();

            var encodedKey = Encoding.ASCII.GetBytes(tokenSettings.SecurityKey);

            return services
                .RegisterIdentityDependencies(encodedKey);
        }

        private static IServiceCollection AddAuthorizationServices(this IServiceCollection services) =>
            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddAuthorization(options =>
                {
                    options.AddPolicy(
                        AuthorizationPolicies.ResourceOwnerIdentityConfirmationRequiredPolicy,
                        policy =>
                            policy
                                .Requirements
                                .Add(
                                    new ResourceOwnerIdentityRequirement(
                                        services.BuildServiceProvider())));

                    options.AddPolicy(
                        AuthorizationPolicies.AdministrativePrivilegesRequiredPolicy,
                        policy =>
                            policy
                                .Requirements
                                .Add(
                                    new RolesManagementPermittedRequirement(
                                        services.BuildServiceProvider())));
                });
    }
}
