using System;
using AsCore.Application.Abstractions.Messaging.Events;

namespace User.Application.Contracts.User.Events
{
    public sealed record UserDeletedIntegrationEvent : IntegrationEvent
    {
        public UserDeletedIntegrationEvent(Guid userId)
            : base(userId,
                nameof(UserDeletedIntegrationEvent))
        {
        }
    }
}
