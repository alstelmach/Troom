using System;
using Core.Application.Abstractions.Messaging.Events;

namespace User.IntegrationEvents.User
{
    public sealed class UserRoleDeniedIntegrationEvent : IntegrationEvent
    {
        public UserRoleDeniedIntegrationEvent(Guid userId,
            Guid roleId)
                : base(userId,
                    nameof(UserRoleDeniedIntegrationEvent)) =>
                        RoleId = roleId;
                        
        public Guid RoleId { get; }
    }
}
