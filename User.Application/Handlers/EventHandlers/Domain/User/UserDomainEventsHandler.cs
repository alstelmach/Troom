using System.Threading;
using System.Threading.Tasks;
using User.Domain.User.Events;
using User.Infrastructure.Persistence.Read.Repositories.User;
using User.IntegrationEvents.User;
using Core.Application.Abstractions.Messaging.Events;

namespace User.Application.Handlers.EventHandlers.Domain.User
{
    public sealed class UserDomainEventsHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;
        private readonly IUserReadModelRepository _userReadModelRepository;

        public UserDomainEventsHandler(IIntegrationEventPublisher integrationEventPublisher,
            IUserReadModelRepository userReadModelRepository)
        {
            _integrationEventPublisher = integrationEventPublisher;
            _userReadModelRepository = userReadModelRepository;
        }
        
        public async Task Handle(UserCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            await AddUserReadModelAsync(@event);
            
            var integrationEvent = new UserCreatedIntegrationEvent(@event.EntityId,
                @event.Login,
                @event.Password,
                @event.FirstName,
                @event.LastName,
                @event.MailAddress);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }

        private async Task AddUserReadModelAsync(UserCreatedDomainEvent @event)
        {
            var user = new global::User.Infrastructure.Persistence.Read.Entities.User(@event.Id,
                @event.Login,
                @event.Password,
                @event.FirstName,
                @event.LastName,
                @event.MailAddress);

            await _userReadModelRepository.CreateAsync(user);
        }
    }
}
