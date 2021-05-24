using System;

namespace User.Application.Dto
{
    public sealed record AuthorizationResultDto(
        Guid UserId,
        Guid PermissionId,
        bool IsAuthorized);
}
