using System;

namespace User.Application.Dto
{
    public sealed class AuthorizationResultDto
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
