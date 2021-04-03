using System;
using System.Security.Claims;
using Core.Application.Abstractions.Messaging.Queries;
using User.Application.Dto.User;

namespace User.Application.Contracts.User.Queries
{
    public sealed class GetUserQuery : IQuery<UserDto>
    {
        public Guid Id { get; init; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
