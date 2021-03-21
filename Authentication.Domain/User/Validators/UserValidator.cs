using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Domain.User.Exceptions;
using Authentication.Domain.User.Repositories;
using FluentValidation;

namespace Authentication.Domain.User.Validators
{
    public sealed class UserValidator : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailAddressValidator _mailAddressValidator;

        public UserValidator(IUserRepository userRepository,
            IMailAddressValidator mailAddressValidator)
        {
            _userRepository = userRepository;
            _mailAddressValidator = mailAddressValidator;

            RuleFor(user => user.Login)
                .MustAsync(BeAUniqueLoginAsync)
                .OnAnyFailure(user => throw new UserLoginUniquenessException(user.Login));

            RuleFor(user => user.MailAddress)
                .Must(BeAValidMailAddress)
                .OnAnyFailure(user => throw new InvalidMailAddressException(user.MailAddress));

            RuleFor(user => user.MailAddress)
                .MustAsync(BeAUniqueMailAddressAsync)
                .OnAnyFailure(user => throw new UserMailAddressUniquenessException(user.MailAddress));
        }
        
        private async Task<bool> BeAUniqueLoginAsync(string login, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync(cancellationToken);
            var isUnique = users.All(user => user.Login != login);
            
            return isUnique;
        }

        private bool BeAValidMailAddress(string mailAddress) =>
            _mailAddressValidator.Validate(mailAddress);

        private async Task<bool> BeAUniqueMailAddressAsync(string mailAddress, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync(cancellationToken);
            var isUnique = users.All(user => user.MailAddress != mailAddress);
            
            return isUnique;
        }
    }
}
