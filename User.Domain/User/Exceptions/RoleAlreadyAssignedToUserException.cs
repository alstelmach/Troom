using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Exceptions
{
    [Serializable]
    public sealed class RoleAlreadyAssignedToUserException : DomainException
    {
        private const string MessagePattern = "Role {0} already assigned to {1} user";
        
        public RoleAlreadyAssignedToUserException(Guid roleId,
            Guid userId)
                : base(string.Format(MessagePattern, roleId, userId))
        {
        }
    }
}
