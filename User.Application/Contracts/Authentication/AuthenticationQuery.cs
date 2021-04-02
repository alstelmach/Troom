using System.Security.Claims;
using Core.Application.Abstractions.Messaging.Queries;
using User.Application.Dto;

namespace User.Application.Contracts.Authentication
{
    public sealed class AuthenticationQuery : IQuery<AuthenticationResultDto>
    {
        public string Login { get; init; }
        public string Password { get; init; }
        public ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
