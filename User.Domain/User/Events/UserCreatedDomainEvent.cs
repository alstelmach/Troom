using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Events
{
    public sealed class UserCreatedDomainEvent : DomainEvent
    {
        public UserCreatedDomainEvent(Guid entityId,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress)
                : base(entityId) =>
                    (Login, Password, FirstName, LastName, MailAddress) = (login, password, firstName, lastName, mailAddress);
        
        public string Login { get; }
        public byte[] Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
    }
}
