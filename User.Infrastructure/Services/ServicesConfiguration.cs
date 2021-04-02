using User.Domain.User.Services;
using User.Domain.User.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure.Services.Encryption;
using User.Infrastructure.Services.TokenGeneration;
using User.Infrastructure.Services.Validators;

namespace User.Infrastructure.Services
{
    internal static class UtilitiesConfiguration
    {
        private const string EncryptionSectionKey = "EncryptionSettings";
        private const string TokenSectionKey = "TokenSettings";
        
        internal static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration) =>
                services
                    .AddScoped<IMailAddressValidator, MailAddressValidator>()
                    .AddScoped<IEncryptionService, EncryptionService>()
                    .AddScoped<TokenGenerationService>()
                    .Configure<EncryptionSettings>(configuration.GetSection(EncryptionSectionKey))
                    .Configure<TokenSettings>(configuration.GetSection(TokenSectionKey));
    }
}
