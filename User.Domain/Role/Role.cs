using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Abstractions.BuildingBlocks;
using User.Domain.Role.Events;
using User.Domain.Role.Exceptions;

namespace User.Domain.Role
{
    public sealed class Role : AggregateRoot
    {
        internal Role(Guid id,
            string name)
                : base(id)
        {
            Name = name;
            Enqueue(new RoleCreatedDomainEvent(Id, Name));
        }
        
        public string Name { get; private set; }
        public ICollection<Guid> Permissions { get; } = new List<Guid>();

        public void AssignPermission(Permission.Permission permission)
        {
            var isAlreadyAssigned = Permissions
                .Any(assignedPermission =>
                    assignedPermission == permission.Id);

            if (isAlreadyAssigned)
            {
                throw new PermissionAlreadyAssignedToRoleException(permission.Id, Id);
            }

            Permissions.Add(permission.Id);
            Enqueue(new PermissionAssignedToRoleDomainEvent(Id, permission.Id));
        }

        private void Apply(RoleCreatedDomainEvent @event) =>
            (Id, Name) = (@event.EntityId, @event.Name);

        private void Apply(PermissionAssignedToRoleDomainEvent @event) =>
            Permissions.Add(@event.PermissionId);
    }
}
