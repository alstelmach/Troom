using System.Security.Claims;
using Core.Application.Abstractions.Messaging.Commands;

namespace Authentication.Application.Contracts.User.Commands
{
    public sealed class CreateUserCommand : ICommand
    {
        public string Login { get; init; }
        public string Password { get; init; }
        public string Name { get; init; }
        public string LastName { get; init; }
        public string MailAddress { get; init; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
