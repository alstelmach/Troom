using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed class RoleAssignedToUserDomainEvent : DomainEvent
    {
        public RoleAssignedToUserDomainEvent(Guid entityId,
            Guid roleId)
                : base(entityId) =>
                    RoleId = roleId;
        
        public Guid RoleId { get; }
    }
}
