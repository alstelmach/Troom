using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddMediatR(Assembly.GetExecutingAssembly());
    }
}
