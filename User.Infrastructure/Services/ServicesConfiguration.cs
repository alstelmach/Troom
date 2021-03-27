using User.Domain.User.Services;
using User.Domain.User.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure.Services.Encryption;
using User.Infrastructure.Services.Validators;

namespace User.Infrastructure.Services
{
    internal static class UtilitiesConfiguration
    {
        private const string EncryptionSectionKey = "EncryptionSettings";
        
        internal static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration) =>
                services
                    .AddScoped<IMailAddressValidator, MailAddressValidator>()
                    .AddScoped<IEncryptionService, EncryptionService>()
                    .Configure<EncryptionSettings>(configuration.GetSection(EncryptionSectionKey));
    }
}
