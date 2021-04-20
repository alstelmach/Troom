using System;
using AsCore.Application.Abstractions.Messaging.Events;

namespace User.Application.Contracts.User.Events
{
    public sealed class PasswordChangedIntegrationEvent : IntegrationEvent
    {
        public PasswordChangedIntegrationEvent(Guid entityId)
            : base(entityId,
                nameof(PasswordChangedIntegrationEvent))
        {
        }
    }
}
