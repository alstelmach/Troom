using Authentication.Application;
using Authentication.Domain;
using Authentication.Infrastructure;
using Core.Infrastructure.HealthCheck;
using Core.Infrastructure.Mvc;
using Core.Utilities.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Api
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
                .UseHealthChecksMiddleware()
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpointsMiddleware();
    }
}
