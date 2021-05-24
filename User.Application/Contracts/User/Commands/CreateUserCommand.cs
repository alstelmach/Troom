using System;
using AsCore.Application.Abstractions.Messaging;
using AsCore.Application.Abstractions.Messaging.Commands;

namespace User.Application.Contracts.User.Commands
{
    public sealed record CreateUserCommand(
        Guid Id,
        string Login,
        string Password,
        string Name,
        string LastName,
        string MailAddress) : Contract, ICommand;
}
