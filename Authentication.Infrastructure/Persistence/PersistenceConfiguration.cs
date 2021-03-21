using Authentication.Domain.User.Repositories;
using Authentication.Infrastructure.Persistence.Repositories;
using Core.Domain.Abstractions.BuildingBlocks;
using Core.Infrastructure.Persistence.PostgreSQL.Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure.Persistence
{
    public static class PersistenceConfiguration
    {
        private const string SchemaName = "AuthenticationContext";
        private const string AuthenticationContextConnectionStringSectionKey = "AuthenticationContext";
        private const string HealthCheckName = "Event-store-check";

        internal static IServiceCollection RegisterPersistenceDependencies(this IServiceCollection services,
            IConfiguration configuration) =>
                services
                    .AddMarten(configuration,
                        SchemaName,
                        AuthenticationContextConnectionStringSectionKey,
                        true,
                        HealthCheckName)
                    .AddScoped<IEventStore, EventStore>()
                    .AddScoped<IUserRepository, UserRepository>();
    }
}
