using System;
using System.Collections.Generic;
using System.Security.Claims;
using Core.Application.Abstractions.Messaging.Queries;
using User.Application.Dto;

namespace User.Application.Contracts.User.Queries
{
    public sealed class GetUserRolesQuery : IQuery<IEnumerable<RoleDto>>
    {
        public Guid UserId { get; init; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
