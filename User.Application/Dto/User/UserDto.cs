using System;

namespace User.Application.Dto.User
{
    public sealed class UserDto
    {
        public Guid Id { get; }
        public string Login { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
        public string JwtToken { get; }
    }
}
