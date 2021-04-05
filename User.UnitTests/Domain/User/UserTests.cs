using System;
using User.Domain.User.Events;
using User.Domain.User.Factories;
using Xunit;

namespace User.UnitTests.Domain.User
{
    public sealed class UserTests
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
            user.ChangePassword(newPassword);
            
            // Assert
            Assert.Equal(newPassword, user.Password);
            Assert.DoesNotContain(user.DequeueDomainEvents(),
                @event => 
                    @event.GetType().Equals(typeof(PasswordChangedDomainEvent)));
        }
    }
}
