using System;
using Core.Application.Abstractions.Messaging.Events;

namespace User.IntegrationEvents.User
{
    public sealed class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public UserCreatedIntegrationEvent(Guid entityId,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress)
                : base(entityId, nameof(UserCreatedIntegrationEvent))
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            MailAddress = mailAddress;
        }

        public string Login { get; }
        public byte[] Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
    }
}
