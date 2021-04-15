using System;
using Core.Application.Abstractions.Messaging.Events;

namespace User.IntegrationEvents.User
{
    public sealed class UserDeletedIntegrationEvent : IntegrationEvent
    {
        public UserDeletedIntegrationEvent(Guid userId)
            : base(userId, nameof(UserDeletedIntegrationEvent))
        {
        }
    }
}
