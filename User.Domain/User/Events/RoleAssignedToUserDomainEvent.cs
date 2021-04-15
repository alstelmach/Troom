using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed class RoleAssignedToUserDomainEvent : DomainEvent
    {
        public RoleAssignedToUserDomainEvent(Guid userId,
            Guid roleId)
                : base(userId) =>
                    RoleId = roleId;
        
        public Guid RoleId { get; }
    }
}
