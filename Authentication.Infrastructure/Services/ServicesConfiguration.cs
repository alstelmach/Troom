using Authentication.Domain.User.Services;
using Authentication.Domain.User.Validators;
using Authentication.Infrastructure.Services.Encryption;
using Authentication.Infrastructure.Services.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure.Services
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
