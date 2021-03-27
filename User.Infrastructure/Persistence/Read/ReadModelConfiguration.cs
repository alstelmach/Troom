using System.Reflection;
using Core.Infrastructure.Persistence.BuildingBlocks;
using Core.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure.Persistence.Read.Context;
using User.Infrastructure.Persistence.Read.Repositories.User;

namespace User.Infrastructure.Persistence.Read
{
    internal static class ReadModelConfiguration
    {
        private const string UserContextConnectionStringSectionKey = "UserContextRead";

        internal static IServiceCollection AddReadModelDependencies(this IServiceCollection services,
            IConfiguration configuration) =>
                services
                    .AddDatabaseContext<UserReadModelContext>(configuration,
                        DatabaseProvider.PostgreSQL,
                        true,
                        UserContextConnectionStringSectionKey,
                        Assembly.GetExecutingAssembly().FullName)
                    .AddScoped<IUserReadModelRepository, UserReadModelRepository>();

        internal static IApplicationBuilder UseReadModelMiddlewares(this IApplicationBuilder builder) =>
            builder
                .UseMigrationsOfContext<UserReadModelContext>();
    }
}
