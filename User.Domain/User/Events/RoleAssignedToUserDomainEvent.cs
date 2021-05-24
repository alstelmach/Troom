using System;
using AsCore.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed record RoleAssignedToUserDomainEvent : DomainEvent
    {
        public RoleAssignedToUserDomainEvent(Guid userId,
            Guid roleId)
                : base(userId) =>
                    RoleId = roleId;
        
        public Guid RoleId { get; }
    }
}
