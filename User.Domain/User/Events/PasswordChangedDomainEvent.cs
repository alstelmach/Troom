using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed class PasswordChangedDomainEvent : DomainEvent
    {
        public PasswordChangedDomainEvent(Guid entityId,
            byte[] newPassword)
                : base(entityId) =>
                    NewPassword = newPassword;
        
        public byte[] NewPassword { get; }
    }
}
