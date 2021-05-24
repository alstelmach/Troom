using System;
using AsCore.Application.Abstractions.Messaging;
using AsCore.Application.Abstractions.Messaging.Queries;
using User.Application.Dto;

namespace User.Application.Contracts.Authorization
{
    public sealed record AuthorizeQuery(Guid PermissionId) : Contract, IQuery<AuthorizationResultDto>;
}
