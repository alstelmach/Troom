using Core.Infrastructure.HealthCheck;
using Core.Infrastructure.Identity;
using Core.Infrastructure.Mvc;
using User.Domain;
using Core.Utilities.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment env) =>
            applicationBuilder
                .UseSwaggerMiddleware(_configuration)
                .UseInfrastructureMiddlewares()
                .UseHealthChecksMiddleware()
                .UseHttpsRedirection()
                .UseRouting()
                .UseIdentityMiddlewares()
                .UseEndpointsMiddleware();
    }
}
