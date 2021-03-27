using System.Text.RegularExpressions;
using FluentValidation;
using User.Domain.User.Exceptions;
using User.Domain.User.ValueObjects;

namespace User.Domain.User.Validators
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
