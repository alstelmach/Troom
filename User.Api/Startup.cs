using System;
using AsCore.Infrastructure.HealthCheck;
using AsCore.Infrastructure.Identity;
using AsCore.Infrastructure.Mvc;
using AsCore.Infrastructure.Mvc.Cors;
using AsCore.Utilities.Swagger;
using User.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application;
using User.Infrastructure;

namespace User.Api
{
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .RegisterDomainDependencies()
                .RegisterApplicationDependencies()
                .RegisterInfrastructureDependencies(_configuration)
                .AddSwagger(_configuration);

        public void Configure(IApplicationBuilder applicationBuilder,
            IServiceProvider serviceProvider) =>
                applicationBuilder
                    .UseSwaggerMiddleware(_configuration)
                    .UseInfrastructureMiddlewares(serviceProvider)
                    .UseHealthChecksMiddleware()
                    .UseHttpsRedirection()
                    .UseCorsMiddlewares()
                    .UseRouting()
                    .UseIdentityMiddlewares()
                    .UseMvcMiddlewares();
    }
}
