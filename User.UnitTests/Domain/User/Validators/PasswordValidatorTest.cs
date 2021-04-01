using System.Threading.Tasks;
using User.Domain.User.Exceptions;
using User.Domain.User.Validators;
using User.Domain.User.ValueObjects;
using Xunit;

namespace User.UnitTests.Domain.User.Validators
{
    public sealed class PasswordValidatorTest
    {
        private readonly PasswordValidator _passwordValidator;

        public PasswordValidatorTest() =>
            _passwordValidator = new PasswordValidator();
        
        [Fact]
        public async Task ShouldAcceptValidPassword()
        {
            // Arrange
            var password = new Password("gR90?csf345,");

            // Act
            var validationResults = await _passwordValidator.ValidateAsync(password);
            
            // Assert
            Assert.Empty(validationResults.Errors);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("1234")]
        [InlineData("09dakfdsamA")]
        [InlineData("FSDAFDSAFDS")]
        [InlineData("password")]
        public async Task ShouldRejectInvalidPassword(string passwordString)
        {
            // Arrange
            var password = new Password(passwordString);
            
            // Act
            Task ValidationTask() => _passwordValidator.ValidateAsync(password);

            // Assert
            await Assert.ThrowsAsync<InvalidPasswordException>(ValidationTask);
        }
    }
}
