using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using User.Domain.User.Factories;
using User.Domain.User.Repositories;
using User.Domain.User.Services;
using User.Domain.User.ValueObjects;
using Xunit;

namespace User.UnitTests.Domain.User.Services
{
    public sealed class UserDomainServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IEncryptionService> _encryptionServiceMock = new();
        private readonly Mock<IValidator<global::User.Domain.User.User>> _userValidatorMock = new();
        private readonly Mock<IValidator<Password>> _passwordValidatorMock = new();
        private readonly UserService _userDomainService;
        
        public UserDomainServiceTest() =>
            _userDomainService = new UserService(
                _userRepositoryMock.Object,
                _encryptionServiceMock.Object,
                _userValidatorMock.Object,
                _passwordValidatorMock.Object);
        
        [Fact]
        public async Task ShouldCreateUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            const string login = "admin";
            const string password = "abc123ABV";
            const string firstName = "Jan";
            const string lastName = "Kowalski";
            const string mailAddress = "jan.kowalski@gmail.com";
            var expectedPasswordHash = Enumerable.Range(0, 128).Select(value => (byte) value).ToArray();

            _encryptionServiceMock
                .Setup(encryptionService =>
                    encryptionService.EncodePassword(password))
                .Returns(expectedPasswordHash);

            _passwordValidatorMock
                .Setup(passwordValidator =>
                    passwordValidator.ValidateAsync(new Password(password), CancellationToken.None))
                .ReturnsAsync(new ValidationResult());

            _userValidatorMock
                .Setup(userValidator =>
                    userValidator.ValidateAsync(It.IsAny<global::User.Domain.User.User>(), CancellationToken.None))
                .ReturnsAsync(new ValidationResult());

            _userRepositoryMock
                .Setup(userRepository =>
                    userRepository.CreateAsync(It.IsAny<global::User.Domain.User.User>(), CancellationToken.None))
                .ReturnsAsync(UserFactory.Create(id, login, expectedPasswordHash, firstName, lastName, mailAddress));

            // Act
            await _userDomainService.CreateUserAsync(id, login, password, firstName, lastName, mailAddress);
        }
    }
}
