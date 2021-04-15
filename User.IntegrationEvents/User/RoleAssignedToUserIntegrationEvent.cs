using System;
using Core.Application.Abstractions.Messaging.Events;

namespace User.IntegrationEvents.User
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
