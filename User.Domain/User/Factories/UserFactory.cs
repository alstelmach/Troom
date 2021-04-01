using System;

namespace User.Domain.User.Factories
{
    public static class UserFactory
    {
        public static User Create(Guid id,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress) =>
                new(id,
                    login,
                    password,
                    firstName,
                    lastName,
                    mailAddress);
    }
}
