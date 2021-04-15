using System;
using System.Threading.Tasks;
using Moq;
using User.Domain.User.Exceptions;
using User.Domain.User.Factories;
using User.Domain.User.Repositories;
using User.Domain.User.Validators;
using Xunit;

namespace User.UnitTests.Domain.User.Validators
{
    public sealed class UserValidatorTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IMailAddressValidator> _mailAddressValidator = new();
        private readonly UserValidator _userValidator;

        public UserValidatorTest()
        {
            _userValidator = new UserValidator(
                _userRepositoryMock.Object,
                _mailAddressValidator.Object);
            
            // ToDo: clean up after implementation
            // _userRepositoryMock
            //     .Setup(repo =>
            //         repo.GetAsync(CancellationToken.None))
            //     .ReturnsAsync(new[]
            //     {
            //         UserFactory.Create(Guid.NewGuid(), "jkowal", Array.Empty<byte>(), "Jan", "Kowalski", "jkowal@gmail.com"),
            //         UserFactory.Create(Guid.NewGuid(), "ajaworek", Array.Empty<byte>(), "Adam", "Jaworek", "ajaworek@gmail.com"),
            //     });

            _mailAddressValidator
                .Setup(validator =>
                    validator.Validate(It.IsAny<string>()))
                .Returns(true);
        }
        
        [Fact]
        public async Task ShouldAcceptValidUser()
        {
            // Arrange
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "astelmach",
                Array.Empty<byte>(),
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");

            // Act
            var validationResults = await _userValidator.ValidateAsync(user);

            // Assert
            Assert.Empty(validationResults.Errors);
        }
        
        [Fact]
        public async Task ShouldRejectTakenLogin()
        {
            // Arrange
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "ajaworek",
                Array.Empty<byte>(),
                "Adam",
                "Nowacki",
                "anowacki@gmail.com");
            
            // Act
            Task ValidationResults() => _userValidator.ValidateAsync(user);
            
            // Arrange
            await Assert.ThrowsAsync<UserLoginUniquenessException>(ValidationResults);
        }
        
        [Fact]
        public async Task ShouldRejectTakenMailAddress()
        {
            // Arrange
            var user = UserFactory.Create(
                Guid.NewGuid(),
                "bnowak",
                Array.Empty<byte>(),
                "Barbara",
                "Nowak",
                "ajaworek@gmail.com");
            
            // Act
            Task ValidationResults() => _userValidator.ValidateAsync(user);
            
            // Arrange
            await Assert.ThrowsAsync<UserMailAddressUniquenessException>(ValidationResults);
        }
    }
}
