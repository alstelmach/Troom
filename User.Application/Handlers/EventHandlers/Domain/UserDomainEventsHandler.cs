using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsCore.Application.Abstractions.Messaging.Events;
using User.Application.Contracts.User.Events;
using User.Application.Dto;
using User.Application.Repositories;
using User.Domain.User.Events;

namespace User.Application.Handlers.EventHandlers.Domain
{
    public sealed class UserDomainEventsHandler : IDomainEventHandler<UserCreatedDomainEvent>,
        IDomainEventHandler<PasswordChangedDomainEvent>,
        IDomainEventHandler<UserDeletedDomainEvent>,
        IDomainEventHandler<RoleAssignedToUserDomainEvent>,
        IDomainEventHandler<UserRoleDeniedDomainEvent>
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
                @event.FirstName,
                @event.LastName,
                @event.MailAddress);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }

        public async Task Handle(PasswordChangedDomainEvent @event, CancellationToken cancellationToken)
        {
            await UpdatePasswordAsync(@event);

            var integrationEvent = new PasswordChangedIntegrationEvent(@event.EntityId);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }
        
        public async Task Handle(UserDeletedDomainEvent @event, CancellationToken cancellationToken)
        {
            await DeleteUserDtoAsync(@event);

            var integrationEvent = new UserDeletedIntegrationEvent(@event.EntityId);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }

        public async Task Handle(RoleAssignedToUserDomainEvent @event, CancellationToken cancellationToken)
        {
            await AssignRoleToUserDtoAsync(@event);

            var integrationEvent = new RoleAssignedToUserIntegrationEvent(@event.EntityId, @event.RoleId);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }

        public async Task Handle(UserRoleDeniedDomainEvent @event, CancellationToken cancellationToken)
        {
            await DenyUserDtoRoleAsync(@event);

            var integrationEvent = new UserRoleDeniedIntegrationEvent(@event.EntityId, @event.RoleId);

            await _integrationEventPublisher.PublishAsync(integrationEvent);
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

        private async Task UpdatePasswordAsync(PasswordChangedDomainEvent @event)
        {
            var user = await _userDtoRepository.GetAsync(@event.EntityId);

            user.Password = @event.NewPassword;

            await _userDtoRepository.UpdateAsync(user);
        }

        private async Task DeleteUserDtoAsync(UserDeletedDomainEvent @event)
        {
            var userDto = await _userDtoRepository.GetAsync(@event.EntityId);
            await _userDtoRepository.DeleteAsync(userDto);
        }

        private async Task AssignRoleToUserDtoAsync(RoleAssignedToUserDomainEvent @event)
        {
            var userDto = await _userDtoRepository.GetAsync(@event.EntityId);
            var roleDto = await _roleDtoRepository.GetAsync(@event.RoleId);

            userDto.Roles.Add(roleDto);

            await _userDtoRepository.UpdateAsync(userDto);
        }

        private async Task DenyUserDtoRoleAsync(UserRoleDeniedDomainEvent @event)
        {
            var user = await _userDtoRepository.GetUserIncludingRolesAsync(@event.EntityId);
            
            var doesExist = user is not null;
            
            if (doesExist)
            {
                user.Roles = user
                    .Roles
                    .Where(role =>
                        role.Id != @event.RoleId)
                    .ToList();

                await _userDtoRepository.UpdateAsync(user);
            }
        }
    }
}
