using System.Text.RegularExpressions;
using Authentication.Domain.User.Exceptions;
using Authentication.Domain.User.ValueObjects;
using FluentValidation;

namespace Authentication.Domain.User.Validators
{
    public sealed class PasswordValidator : AbstractValidator<Password>
    {
        private const string PasswordValidationRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        
        public PasswordValidator()
        {
            RuleFor(password => password.Content)
                .Must(SatisfyPasswordSecurityRules)
                .OnAnyFailure(password => throw new InvalidPasswordException());
        }

        private static bool SatisfyPasswordSecurityRules(string password)
        {
            var regex = new Regex(PasswordValidationRegex);
            var isPasswordValid = regex.IsMatch(password);

            return isPasswordValid;
        }
    }
}
