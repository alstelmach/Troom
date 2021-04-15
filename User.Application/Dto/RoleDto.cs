using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace User.Application.Dto
{
    public sealed class RoleDto
    {
        public RoleDto(Guid id,
            string name) =>
                (Id, Name) = (id, name);
        
        public Guid Id { get; }
        public string Name { get; }
        [JsonIgnore]
        public ICollection<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
