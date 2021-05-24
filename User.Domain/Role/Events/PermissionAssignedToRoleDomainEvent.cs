using System;
using AsCore.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.Role.Events
{
    public sealed record PermissionAssignedToRoleDomainEvent : DomainEvent
    {
        internal PermissionAssignedToRoleDomainEvent(Guid entityId,
            Guid permissionId)
                : base(entityId) =>
                    PermissionId = permissionId;
        
        public Guid PermissionId { get; }
    }
}
