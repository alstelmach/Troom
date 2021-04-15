using System;
using System.Security.Claims;
using Core.Application.Abstractions.Messaging.Queries;
using User.Application.Dto;

namespace User.Application.Contracts.Authorization
{
    public sealed class AuthorizeQuery : IQuery<AuthorizationResultDto>
    {
        public Guid PermissionId { get; init; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
