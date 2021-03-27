using System;

namespace User.Domain.User.Factories
{
    public static class UserFactory
    {
        public static User Create(string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress) =>
                new(Guid.NewGuid(),
                    login,
                    password,
                    firstName,
                    lastName,
                    mailAddress);
    }
}
