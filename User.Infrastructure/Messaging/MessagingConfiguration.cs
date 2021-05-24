using AsCore.Infrastructure.Messaging;
using AsCore.Infrastructure.Messaging.MessageBrokers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application;

namespace User.Infrastructure.Messaging
{
    internal static class MessagingConfiguration
    {
        internal static IServiceCollection RegisterMessagingDependencies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration) =>
                serviceCollection
                    .AddDomesticMessaging()
                    .AddIntegrationMessaging(configuration,
                        MessageBroker.RabbitMQ,
                        true,
                        typeof(ApplicationConfiguration).Assembly); // ToDo: Probably may find a little better type for obtaining the handlers assembly
    }
}
