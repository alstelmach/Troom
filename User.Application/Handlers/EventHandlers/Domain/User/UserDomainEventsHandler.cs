using System.Threading;
using System.Threading.Tasks;
using User.Domain.User.Events;
using User.IntegrationEvents.User;
using Core.Application.Abstractions.Messaging.Events;
using User.Application.Dto.User;

namespace User.Application.Handlers.EventHandlers.Domain.User
{
    public sealed class UserDomainEventsHandler : IDomainEventHandler<UserCreatedDomainEvent>,
        IDomainEventHandler<PasswordChangedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;
        private readonly IUserDtoRepository _userDtoRepository;

        public UserDomainEventsHandler(IIntegrationEventPublisher integrationEventPublisher,
            IUserDtoRepository userDtoRepository)
        {
            _integrationEventPublisher = integrationEventPublisher;
            _userDtoRepository = userDtoRepository;
        }
        
        public async Task Handle(UserCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            await SaveUserDtoAsync(@event);
            
            var integrationEvent = new UserCreatedIntegrationEvent(@event.EntityId,
                @event.Login,
                @event.Password,
                @event.FirstName,
                @event.LastName,
                @event.MailAddress);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }

        public async Task Handle(PasswordChangedDomainEvent @event, CancellationToken cancellationToken)
        {
            var user = await _userDtoRepository.GetAsync(@event.EntityId, cancellationToken);

            user.Password = @event.NewPassword;
            await _userDtoRepository.UpdateAsync(user);
        }

        private async Task SaveUserDtoAsync(UserCreatedDomainEvent @event)
        {
            var user = new UserDto(@event.EntityId,
                @event.Login,
                @event.Password,
                @event.FirstName,
                @event.LastName,
                @event.MailAddress);

            await _userDtoRepository.CreateAsync(user);
        }
    }
}
