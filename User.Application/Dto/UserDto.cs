using System;

namespace User.Application.Dto
{
    public sealed class UserDto
    {
        public Guid Id { get; init; }
        public string Login { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string MailAddress { get; init; }

        public static explicit operator UserDto(Infrastructure.Persistence.Read.Entities.User user) =>
            user is not null
                ? new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MailAddress = user.MailAddress
                }
                : null;
    }
}
