using User.Infrastructure.Persistence.Read;
using User.Infrastructure.Persistence.Write;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace User.Infrastructure.Persistence
{
    internal static class PersistenceConfiguration
    {
        internal static IServiceCollection RegisterPersistenceDependencies(this IServiceCollection services,
            IConfiguration configuration) =>
                services
                    .AddReadModelDependencies(configuration)
                    .AddWriteModelDependencies(configuration);
    }
}
