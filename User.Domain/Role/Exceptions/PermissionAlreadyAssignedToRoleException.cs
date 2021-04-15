using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.Role.Exceptions
{
    [Serializable]
    public sealed class PermissionAlreadyAssignedToRoleException : DomainException
    {
        private const string MessagePattern = "Permission {0} already assigned to {1} role";

        internal PermissionAlreadyAssignedToRoleException(Guid permissionId,
            Guid roleId)
                : base(string.Format(MessagePattern, permissionId, roleId))
        {
        }
    }
}
