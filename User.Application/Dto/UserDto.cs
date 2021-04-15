using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace User.Application.Dto
{
    public sealed class UserDto
    {
        public UserDto(Guid id,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress)
        {
            Id = id;
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            MailAddress = mailAddress;
        }
        
        private UserDto() {}
        
        public Guid Id { get; }
        public string Login { get; }
        [JsonIgnore]
        public byte[] Password { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MailAddress { get; }
        [JsonIgnore]
        public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
