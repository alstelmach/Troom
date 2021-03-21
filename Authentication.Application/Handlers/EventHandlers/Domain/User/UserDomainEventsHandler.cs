using System.Threading;
using System.Threading.Tasks;
using Authentication.Domain.User.Events;
using Authentication.IntegrationEvents.User;
using Core.Application.Abstractions.Messaging.Events;

namespace Authentication.Application.Handlers.EventHandlers.Domain.User
{
    public sealed class UserDomainEventsHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        public UserDomainEventsHandler(IIntegrationEventPublisher integrationEventPublisher)
        {
            _integrationEventPublisher = integrationEventPublisher;
        }
        
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new UserCreatedIntegrationEvent(notification.EntityId,
                notification.Login,
                notification.Password,
                notification.FirstName,
                notification.LastName,
                notification.MailAddress);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }
    }
}
