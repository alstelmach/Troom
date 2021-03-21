using System.Threading.Tasks;
using Authentication.Domain.User.Factories;
using Authentication.Domain.User.Repositories;
using Authentication.Domain.User.ValueObjects;
using FluentValidation;

namespace Authentication.Domain.User.Services
{
    public sealed class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<Password> _passwordValidator;

        public UserService(IUserRepository userRepository,
            IEncryptionService encryptionService,
            IValidator<User> userValidator,
            IValidator<Password> passwordValidator)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
        }
        
        public async Task CreateUserAsync(string login,
            string password,
            string firstName,
            string lastName,
            string mailAddress)
        {
            await _passwordValidator.ValidateAsync(new Password(password));
            var passwordHash = _encryptionService.EncodePassword(password);
            
            var user = UserFactory.Create(login,
                passwordHash,
                firstName,
                lastName,
                mailAddress);

            await _userValidator.ValidateAsync(user);
            await _userRepository.CreateAsync(user);
        }
    }
}
