using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using User.Application.Dto.User;

namespace User.Application.Dto.Role
{
    public sealed class RoleDto
    {
        public RoleDto(Guid id,
            string name) =>
                (Id, Name) = (id, Name);
        
        public Guid Id { get; }
        public string Name { get; }
        [JsonIgnore]
        public ICollection<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
