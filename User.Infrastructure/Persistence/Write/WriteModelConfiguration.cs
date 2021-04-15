using User.Domain.User.Repositories;
using Core.Domain.Abstractions.BuildingBlocks;
using Core.Infrastructure.Persistence.Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Domain.Permission.Repositories;
using User.Domain.Role.Repositories;
using User.Infrastructure.Persistence.Write.Repositories;

namespace User.Infrastructure.Persistence.Write
{
    internal static class WriteModelConfiguration
    {
        private const string SchemaName = "UserWriteModel";
        private const string UserContextConnectionStringSectionKey = "UserContextWrite";
        private const string HealthCheckName = "Event-store-check";
        
        internal static IServiceCollection AddWriteModelDependencies(this IServiceCollection services,
            IConfiguration configuration) =>
                services
                    .AddMarten(configuration,
                        SchemaName,
                        UserContextConnectionStringSectionKey,
                        true,
                        HealthCheckName)
                    .AddScoped<IEventStore, EventStore>()
                    .AddScoped<IPermissionRepository, PermissionRepository>()
                    .AddScoped<IRoleRepository, RoleRepository>()
                    .AddScoped<IUserRepository, UserRepository>();
    }
}
