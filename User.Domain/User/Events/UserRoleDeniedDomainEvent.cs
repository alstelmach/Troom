using System;
using AsCore.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed record UserRoleDeniedDomainEvent : DomainEvent
    {
        public UserRoleDeniedDomainEvent(Guid entityId,
            Guid roleId)
                : base(entityId) =>
                    RoleId = roleId;
        
        public Guid RoleId { get; }
    }
}
