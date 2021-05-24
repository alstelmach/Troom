using System;
using AsCore.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed record PasswordChangedDomainEvent : DomainEvent
    {
        public PasswordChangedDomainEvent(Guid entityId,
            byte[] newPassword)
                : base(entityId) =>
                    NewPassword = newPassword;
        
        public byte[] NewPassword { get; }
    }
}
