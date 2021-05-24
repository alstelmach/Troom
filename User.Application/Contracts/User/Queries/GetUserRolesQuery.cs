using System;
using System.Collections.Generic;
using AsCore.Application.Abstractions.Messaging;
using AsCore.Application.Abstractions.Messaging.Queries;
using User.Application.Dto;

namespace User.Application.Contracts.User.Queries
{
    public sealed record GetUserRolesQuery(Guid UserId) : Contract, IQuery<IEnumerable<RoleDto>>;
}
