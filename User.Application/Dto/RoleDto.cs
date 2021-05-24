using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace User.Application.Dto
{
    public sealed record RoleDto(Guid Id, string Name)
    {
        [JsonIgnore]
        public ICollection<UserDto> Users { get; } = new List<UserDto>();
    }
}
