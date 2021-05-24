using AsCore.Application.Abstractions.Messaging;
using AsCore.Application.Abstractions.Messaging.Commands;

namespace User.Application.Contracts.User.Commands
{
    public sealed record DeleteUserCommand : Contract, ICommand;
}
