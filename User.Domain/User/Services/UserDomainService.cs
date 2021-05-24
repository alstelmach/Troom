using System;
using System.Threading.Tasks;
using FluentValidation;
using User.Domain.User.Factories;
using User.Domain.User.Repositories;
using User.Domain.User.ValueObjects;

namespace User.Domain.User.Services
{
    public sealed class UserDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<Password> _passwordValidator;

        public UserDomainService(IUserRepository userRepository,
            IEncryptionService encryptionService,
            IValidator<User> userValidator,
            IValidator<Password> passwordValidator)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
        }
        
        public async Task CreateUserAsync(Guid id,
            string login,
            string password,
            string firstName,
            string lastName,
            string mailAddress)
        {
            var passwordHash = await EncodePasswordAsync(password);
            
            var user = UserFactory.Create(id,
                login,
                passwordHash,
                firstName,
                lastName,
                mailAddress);

            await _userValidator.ValidateAsync(user);
            await _userRepository.CreateAsync(user);
        }

        public async Task ChangeUserPasswordAsync(Guid userId,
            string password)
        {
            var user = await _userRepository.GetAsync(userId);
            var encodedPassword = await EncodePasswordAsync(password);
            
            user.ChangePassword(encodedPassword);
            await _userRepository.UpdateAsync(user);
        }

        private async Task<byte[]> EncodePasswordAsync(string password)
        {
            await _passwordValidator.ValidateAsync(new Password(password));
            var passwordHash = _encryptionService.EncodePassword(password);

            return passwordHash;
        }
    }
}
