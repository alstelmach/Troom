using System.Collections.Generic;
using AsCore.Application.Abstractions.Messaging;
using AsCore.Application.Abstractions.Messaging.Queries;
using User.Application.Dto;

namespace User.Application.Contracts.User.Queries
{
    public sealed record GetUsersQuery : Contract, IQuery<IEnumerable<UserDto>>;
}
