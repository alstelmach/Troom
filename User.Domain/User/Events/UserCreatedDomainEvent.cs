using System;
using AsCore.Domain.Abstractions.BuildingBlocks;
using User.Domain.User.Enumerations;

namespace User.Domain.User.Events
{
    public sealed record UserCreatedDomainEvent : DomainEvent
    {
        public UserCreatedDomainEvent(Guid entityId,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress,
            UserStatus userStatus)
                : base(entityId)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            MailAddress = mailAddress;
            Status = userStatus;
        }

        public string Login { get; }
        public byte[] Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
        public UserStatus Status { get; } 
    }
}
