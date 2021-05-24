using System;
using AsCore.Application.Abstractions.Messaging.Events;

namespace User.Application.Contracts.User.Events
{
    public sealed record UserCreatedIntegrationEvent : IntegrationEvent
    {
        public UserCreatedIntegrationEvent(Guid id,
            string login,
            string firstName,
            string lastName,
            string mailAddress)
                : base(id,
                    nameof(UserCreatedIntegrationEvent))
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            MailAddress = mailAddress;
        }

        public string Login { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
    }
}
