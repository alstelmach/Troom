using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Contracts.User.Commands;
using Authentication.Domain.User.Services;
using Core.Application.Abstractions.Messaging.Commands;
using MediatR;

namespace Authentication.Application.Handlers.CommandHandlers
{
    public sealed class UserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly UserService _userService;

        public UserCommandHandler(UserService userService)
        {
            _userService = userService;
        }
        
        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await _userService.CreateUserAsync(command.Login,
                command.Password,
                command.Name,
                command.LastName,
                command.MailAddress);

            return Unit.Value;
        }
    }
}
