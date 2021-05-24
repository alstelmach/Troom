using System;
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
    public sealed class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IEncryptionService> _encryptionServiceMock = new();
        private readonly Mock<IValidator<global::User.Domain.User.User>> _userValidatorMock = new();
        private readonly Mock<IValidator<Password>> _passwordValidatorMock = new();
        private readonly UserDomainService _userService;

        public UserServiceTest()
        {
            _userService = new UserDomainService(
                _userRepositoryMock.Object,
                _encryptionServiceMock.Object,
                _userValidatorMock.Object,
                _passwordValidatorMock.Object);

            _passwordValidatorMock
                .Setup(passwordValidator =>
                    passwordValidator.ValidateAsync(It.IsAny<Password>(), CancellationToken.None))
                .ReturnsAsync(new ValidationResult());

            _encryptionServiceMock
                .Setup(encryptionService =>
                    encryptionService.EncodePassword(It.IsAny<string>()))
                .Returns(Array.Empty<byte>());
        }

        [Fact]
        public async Task ShouldCreateUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            const string Login = "admin";
            const string Password = "1234ABCd?";
            const string FirstName = "Jan";
            const string LastName = "Kowalski";
            const string MailAddress = "jan.kowalski@gmail.com";
            var user = UserFactory.Create(
                id,
                Login,
                Array.Empty<byte>(),
                FirstName,
                LastName,
                MailAddress);

            _userValidatorMock
                .Setup(userValidator =>
                    userValidator.ValidateAsync(It.IsAny<global::User.Domain.User.User>(), CancellationToken.None))
                .ReturnsAsync(new ValidationResult());

            _userRepositoryMock
                .Setup(userRepository =>
                    userRepository.CreateAsync(It.IsAny<global::User.Domain.User.User>(), CancellationToken.None))
                .ReturnsAsync(user);

            // Act
            await _userService.CreateUserAsync(id, Login, Password, FirstName, LastName, MailAddress);
        }

        [Fact]
        public async Task ShouldChangeUserPassword()
        {
            // Arrange
            var id = Guid.NewGuid();
            const string Password = "1234ABCd?";

            _userRepositoryMock
                .Setup(userRepository =>
                    userRepository.GetAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(UserFactory.Create(
                    id,
                    "astelmach",
                    new byte[] { 1, 2, 3 },
                    "Aleksander",
                    "Stelmach",
                    "alstelmach@outlook.com"));

            _userRepositoryMock
                .Setup(userRepository =>
                    userRepository.UpdateAsync(It.IsAny<global::User.Domain.User.User>(), CancellationToken.None))
                .ReturnsAsync(UserFactory.Create(
                    id,
                    "astelmach",
                    Array.Empty<byte>(),
                    "Aleksander",
                    "Stelmach",
                    "alstelmach@outlook.com"));

            // Act
            await _userService.ChangeUserPasswordAsync(id, Password);
        }
    }
}
