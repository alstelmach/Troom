using User.Infrastructure.Messaging;
using User.Infrastructure.Persistence;
using User.Infrastructure.Services;
using Core.Infrastructure.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using User.Infrastructure.Persistence.Read;

namespace User.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        private const string SelfCheckName = "Self-check";

        public static IServiceCollection RegisterInfrastructureDependencies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration) =>
                serviceCollection
                    .AddHealthChecks()
                    .AddCheck(SelfCheckName, () => HealthCheckResult.Healthy())
                    .Services
                    .RegisterMessagingDependencies(configuration)
                    .RegisterPersistenceDependencies(configuration)
                    .AddMvcDependencies()
                    .RegisterInfrastructureServices(configuration);

        public static IApplicationBuilder UseInfrastructureMiddlewares(this IApplicationBuilder builder) =>
            builder
                .UseReadModelMiddlewares();
    }
}
