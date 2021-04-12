using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Events;
using User.Application.Dto.Role;
using User.Application.Dto.User;
using User.Domain.User.Events;
using User.IntegrationEvents.User;

namespace User.Application.Handlers.EventHandlers.Domain
{
    public sealed class UserDomainEventsHandler : IDomainEventHandler<UserCreatedDomainEvent>,
        IDomainEventHandler<PasswordChangedDomainEvent>,
        IDomainEventHandler<UserDeletedDomainEvent>,
        IDomainEventHandler<RoleAssignedToUserDomainEvent>,
        IDomainEventHandler<RoleDeniedFromUserDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;
        private readonly IUserDtoRepository _userDtoRepository;
        private readonly IRoleDtoRepository _roleDtoRepository;

        public UserDomainEventsHandler(IIntegrationEventPublisher integrationEventPublisher,
            IUserDtoRepository userDtoRepository,
            IRoleDtoRepository roleDtoRepository)
        {
            _integrationEventPublisher = integrationEventPublisher;
            _userDtoRepository = userDtoRepository;
            _roleDtoRepository = roleDtoRepository;
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
            var user = await _userDtoRepository.GetAsync(@event.EntityId, false, cancellationToken);

            user.Password = @event.NewPassword;
            await _userDtoRepository.UpdateAsync(user);
        }
        
        public async Task Handle(UserDeletedDomainEvent @event, CancellationToken cancellationToken)
        {
            var userDto = await _userDtoRepository.GetAsync(@event.EntityId, false, cancellationToken);
            await _userDtoRepository.DeleteAsync(userDto);
        }

        public async Task Handle(RoleAssignedToUserDomainEvent @event, CancellationToken cancellationToken)
        {
            var userDto = await _userDtoRepository.GetAsync(@event.EntityId, false, cancellationToken);
            var roleDto = await _roleDtoRepository.GetAsync(@event.RoleId, cancellationToken);

            userDto.Roles.Add(roleDto);

            await _userDtoRepository.UpdateAsync(userDto);
        }

        public async Task Handle(RoleDeniedFromUserDomainEvent @event, CancellationToken cancellationToken)
        {
            var userDto = await _userDtoRepository.GetAsync(@event.EntityId, true, cancellationToken);

            userDto.Roles = userDto
                .Roles
                .Where(role =>
                    role.Id != @event.RoleId)
                .ToList();

            await _userDtoRepository.UpdateAsync(userDto);
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
