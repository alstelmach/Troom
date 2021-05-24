using System;
using AsCore.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed record UserDeletedDomainEvent : DomainEvent
    {
        public UserDeletedDomainEvent(Guid entityId)
            : base(entityId)
        {
        }
    }
}
