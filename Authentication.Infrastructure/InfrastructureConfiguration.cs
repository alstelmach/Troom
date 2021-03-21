using Authentication.Infrastructure.Messaging;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Services;
using Core.Infrastructure.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Authentication.Infrastructure
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
    }
}
