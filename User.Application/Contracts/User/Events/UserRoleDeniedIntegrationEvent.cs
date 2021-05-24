using System;
using AsCore.Application.Abstractions.Messaging.Events;

namespace User.Application.Contracts.User.Events
{
    public sealed record UserRoleDeniedIntegrationEvent : IntegrationEvent
    {
        public UserRoleDeniedIntegrationEvent(Guid userId,
            Guid roleId)
                : base(userId,
                    nameof(UserRoleDeniedIntegrationEvent)) =>
                        RoleId = roleId;
                        
        public Guid RoleId { get; }
    }
}
