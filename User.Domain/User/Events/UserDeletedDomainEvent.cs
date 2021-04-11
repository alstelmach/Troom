using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed class UserDeletedDomainEvent : DomainEvent
    {
        public UserDeletedDomainEvent(Guid entityId)
            : base(entityId)
        {
        }
    }
}
