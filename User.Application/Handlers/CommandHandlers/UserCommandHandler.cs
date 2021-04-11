using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.User.Services;
using Core.Application.Abstractions.Messaging.Commands;
using Core.Infrastructure.Identity;
using MediatR;
using User.Application.Contracts.User.Commands;
using User.Domain.User.Repositories;

namespace User.Application.Handlers.CommandHandlers
{
    public sealed class UserCommandHandler : ICommandHandler<CreateUserCommand>,
        ICommandHandler<ChangeUserPasswordCommand>,
        ICommandHandler<DeleteUserCommand>
    {
        private readonly UserService _userService;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(UserService userService,
            IEncryptionService encryptionService,
            IUserRepository userRepository)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _userRepository = userRepository;
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
    }
}
