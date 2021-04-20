using System;
using AsCore.Application.Abstractions.Messaging.Events;

namespace User.Application.Contracts.User.Events
{
    public sealed class RoleAssignedToUserIntegrationEvent : IntegrationEvent
    {
        public RoleAssignedToUserIntegrationEvent(Guid userId,
            Guid roleId)
                : base(userId,
                    nameof(RoleAssignedToUserIntegrationEvent)) =>
                        RoleId = roleId;
                        
        public Guid RoleId { get; }
    }
}
