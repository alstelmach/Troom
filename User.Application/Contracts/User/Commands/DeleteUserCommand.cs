using System.Security.Claims;
using Core.Application.Abstractions.Messaging.Commands;

namespace User.Application.Contracts.User.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
