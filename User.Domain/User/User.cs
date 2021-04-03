using System;
using Core.Domain.Abstractions.BuildingBlocks;
using User.Domain.User.Events;

namespace User.Domain.User
{
    public sealed class User : AggregateRoot
    {
        internal User(Guid id,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress)
            : base(id)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            MailAddress = mailAddress;
            Enqueue(new UserCreatedDomainEvent(id,
                Login,
                Password,
                FirstName,
                LastName,
                MailAddress));
        }

        private User()
            : base(Guid.Empty)
        {
        }

        public string Login { get; private set; }
        public byte[] Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MailAddress { get; private set; }

        public void ChangePassword(byte[] password)
        {
            var areTheSame = Password == password;
            
            if (areTheSame)
            {
                return;
            }

            Password = password;
            Enqueue(new PasswordChangedDomainEvent(Id, Password));
        }

        private void Apply(UserCreatedDomainEvent @event)
        {
            Id = @event.EntityId;
            Login = @event.Login;
            Password = @event.Password;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            MailAddress = @event.MailAddress;
        }

        private void Apply(PasswordChangedDomainEvent @event) =>
            Password = @event.NewPassword;
    }
}
