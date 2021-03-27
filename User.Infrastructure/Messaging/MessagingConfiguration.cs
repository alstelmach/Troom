using Core.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace User.Infrastructure.Messaging
{
    internal static class MessagingConfiguration
    {
        internal static IServiceCollection RegisterMessagingDependencies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration) =>
                serviceCollection
                    .AddMessaging()
                    .AddRabbitMQ(configuration,
                        ExchangeType.Fanout,
                        true);
    }
}
