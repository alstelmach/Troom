using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Abstractions.BuildingBlocks;
using User.Infrastructure.Persistence.Read.Context;

namespace User.Infrastructure.Persistence.Read.Entities
{
    [Table("Users", Schema = UserReadModelContext.SchemaName)]
    public sealed class User : Entity
    {
        public User(Guid id,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress)
                : base(id)
        {
            Id = id;
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
