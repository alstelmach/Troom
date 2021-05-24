using AsCore.Application.Abstractions.Messaging;
using AsCore.Application.Abstractions.Messaging.Queries;
using User.Application.Dto;

namespace User.Application.Contracts.Authentication
{
    public sealed record AuthenticationQuery(string Login, string Password) : Contract, IQuery<AuthenticationResultDto>;
}
