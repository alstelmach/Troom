using System;
using System.Threading;
using System.Threading.Tasks;
using AsCore.Application.Abstractions.Identity;
using AsCore.Application.Abstractions.Messaging.Commands;
using User.Domain.User.Services;
using MediatR;
using User.Application.Contracts.User.Commands;
using User.Domain.Role.Repositories;
using User.Domain.User.Repositories;

namespace User.Application.Handlers.CommandHandlers
{
    public sealed class UserCommandHandler : ICommandHandler<CreateUserCommand>,
        ICommandHandler<ChangeUserPasswordCommand>,
        ICommandHandler<DeleteUserCommand>,
        ICommandHandler<AssignRoleToUserCommand>,
        ICommandHandler<DenyUserRoleCommand>
    {
        private readonly UserDomainService _userService;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserCommandHandler(UserDomainService userService,
            IEncryptionService encryptionService,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        
        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await _userService.CreateUserAsync(command.Id,
                command.Login,
                command.Password,
                command.Name,
                command.LastName,
                command.MailAddress);

            return Unit.Value;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
        {
            var ownerId = command.ClaimsPrincipal.GetOwnerId();
            var user = await _userRepository.GetAsync(ownerId, cancellationToken);
            var isAuthenticated = _encryptionService.VerifyPassword(user.Password, command.Password);

            if (!isAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }

            await _userService.ChangeUserPasswordAsync(ownerId, command.NewPassword);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var ownerId = command.ClaimsPrincipal.GetOwnerId();
            var user = await _userRepository.GetAsync(ownerId, cancellationToken);

            user.DeleteUser();
            await _userRepository.UpdateAsync(user, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(AssignRoleToUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId, cancellationToken);
            var role = await _roleRepository.GetAsync(command.RoleId, cancellationToken);

            user.AssignRole(role);
            await _userRepository.UpdateAsync(user, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DenyUserRoleCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId, cancellationToken);
            var role = await _roleRepository.GetAsync(command.RoleId, cancellationToken);

            user.DenyRole(role);
            await _userRepository.UpdateAsync(user, cancellationToken);

            return Unit.Value;
        }
    }
}
