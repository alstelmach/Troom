using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Abstractions.BuildingBlocks;
using User.Domain.User.Enumerations;
using User.Domain.User.Events;
using User.Domain.User.Exceptions;

namespace User.Domain.User
{
    public sealed class User : AggregateRoot
    {
        internal User(Guid id,
            string login,
            byte[] password,
            string firstName,
            string lastName,
            string mailAddress)
                : base(id)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            MailAddress = mailAddress;
            Enqueue(new UserCreatedDomainEvent(id,
                Login,
                Password,
                FirstName,
                LastName,
                MailAddress,
                Status));
        }

        private User()
            : base(Guid.Empty)
        {
        }

        public string Login { get; private set; }
        public byte[] Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MailAddress { get; private set; }
        public UserStatus Status { get; private set; } = UserStatus.Active;
        public IReadOnlyCollection<Guid> Roles { get; private set; } = new List<Guid>();

        public void ChangePassword(byte[] password)
        {
            var areTheSame = Password.SequenceEqual(password);

            if (areTheSame)
            {
                throw new PasswordVirtualChangeException();
            }

            Password = password;
            Enqueue(new PasswordChangedDomainEvent(Id, Password));
        }

        public void DeleteUser()
        {
            Status = UserStatus.Deleted;
            Enqueue(new UserDeletedDomainEvent(Id));
        }

        public void AssignRole(Role.Role role)
        {
            var isAlreadyAssigned = Roles
                .Any(assignedRole =>
                    assignedRole == role.Id);

            if (isAlreadyAssigned)
            {
                throw new RoleAlreadyAssignedToUserException(role.Id, Id);
            }

            AssignRole(role.Id);
            Enqueue(new RoleAssignedToUserDomainEvent(Id, role.Id));
        }

        public void DenyRole(Role.Role role)
        {
            var rolesCount = Roles.Count;

            DenyRole(role.Id);

            var hasBeenDenied = rolesCount == Roles.Count + 1;

            if (hasBeenDenied)
            {
                Enqueue(new UserRoleDeniedDomainEvent(Id, role.Id));
            }
        }

        private void Apply(UserCreatedDomainEvent @event)
        {
            Id = @event.EntityId;
            Login = @event.Login;
            Password = @event.Password;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            MailAddress = @event.MailAddress;
            Status = UserStatus.Active;
        }

        private void Apply(PasswordChangedDomainEvent @event) =>
            Password = @event.NewPassword;

        private void Apply(UserDeletedDomainEvent @event) =>
            Status = UserStatus.Deleted;

        private void Apply(RoleAssignedToUserDomainEvent @event) =>
            AssignRole(@event.RoleId);

        private void Apply(UserRoleDeniedDomainEvent @event) =>
            DenyRole(@event.RoleId);

        private void AssignRole(Guid roleId)
        {
            var mutableRolesList = Roles.ToList();
            mutableRolesList.Add(roleId);
            Roles = mutableRolesList;
        }

        private void DenyRole(Guid roleId) =>
            Roles = Roles
                .Where(assignedRole =>
                    assignedRole != roleId)
                .ToList();
    }
}
