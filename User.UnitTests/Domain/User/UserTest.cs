using System;
using User.Domain.Role.Factories;
using User.Domain.User.Events;
using User.Domain.User.Exceptions;
using User.Domain.User.Factories;
using Xunit;

namespace User.UnitTests.Domain.User
{
    public sealed class UserTest
    {
        [Fact]
        public static void ShouldChangeUserPassword()
        {
            // Arrange
            var newPassword = new byte[] { 1, 2, 3, 4 };
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "astelmach",
                new byte[] { 4, 3, 2, 1 },
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");
            
            // Act
            user.ChangePassword(newPassword);
            
            // Assert
            Assert.Equal(newPassword, user.Password);
            Assert.Contains(user.DequeueDomainEvents(),
                @event => 
                    @event.GetType().Equals(typeof(PasswordChangedDomainEvent)));
        }
        
        [Fact]
        public static void ShouldNotChangeUserPasswordIfTheSame()
        {
            // Arrange
            var newPassword = new byte[] { 1, 2, 3, 4 };
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "astelmach",
                new byte[] { 1, 2, 3, 4 },
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");
            
            // Act
            void PasswordChangeAction() => user.ChangePassword(newPassword);

            // Assert
            Assert.Throws<PasswordVirtualChangeException>(PasswordChangeAction);
            Assert.DoesNotContain(user.DequeueDomainEvents(),
                @event => 
                    @event.GetType().Equals(typeof(PasswordChangedDomainEvent)));
        }

        [Fact]
        public static void ShouldDeleteUser()
        {
            // Arrange
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "astelmach",
                new byte[] { 1, 2, 3, 4 },
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");
            
            // Act
            user.DeleteUser();
            
            // Assert
            Assert.Contains(user.DequeueDomainEvents(),
                @event => 
                    @event.GetType().Equals(typeof(UserDeletedDomainEvent)));
        }

        [Fact]
        public static void ShouldAssignRoleToUser()
        {
            // Arrange
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "astelmach",
                new byte[] { 1, 2, 3, 4 },
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");

            var roleId = Guid.NewGuid();
            var role = RoleFactory.Create(roleId, string.Empty);

            // Act
            user.AssignRole(role);

            // Assert
            Assert.Contains(user.Roles,
                role => role == roleId);
        }

        [Fact]
        public static void ShouldNotDuplicateAssignedRole()
        {
            // Arrange
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "astelmach",
                new byte[] { 1, 2, 3, 4 },
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");

            var roleId = Guid.NewGuid();
            var assignedRole = RoleFactory.Create(roleId, string.Empty);
            user.AssignRole(assignedRole);

            var anotherRole = RoleFactory.Create(roleId, string.Empty);

            // Act
            void AssignAction() => user.AssignRole(anotherRole);

            // Assert
            Assert.Throws<RoleAlreadyAssignedToUserException>(AssignAction);
        }
        
        [Fact]
        public static void ShouldDenyUserRole()
        {
            // Arrange
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "astelmach",
                new byte[] { 1, 2, 3, 4 },
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");

            var assignedRole = RoleFactory.Create(Guid.NewGuid(), string.Empty);
            user.AssignRole(assignedRole);

            // Act
            user.DenyRole(assignedRole);

            // Assert
            Assert.Empty(user.Roles);
        }
    }
}
