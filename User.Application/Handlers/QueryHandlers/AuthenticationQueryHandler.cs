using System.Threading;
using System.Threading.Tasks;
using AsCore.Application.Abstractions.Messaging.Queries;
using User.Application.Contracts.Authentication;
using User.Application.Dto;
using User.Application.Repositories;
using User.Application.Services;
using User.Domain.User.Services;

namespace User.Application.Handlers.QueryHandlers
{
    public sealed class AuthenticationQueryHandler : IQueryHandler<AuthenticationQuery, AuthenticationResultDto>
    {
        private readonly IUserDtoRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenGenerationService _tokenGenerationService;

        public AuthenticationQueryHandler(IUserDtoRepository userRepository,
            IEncryptionService encryptionService,
            ITokenGenerationService tokenGenerationService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _tokenGenerationService = tokenGenerationService;
        }
        
        public async Task<AuthenticationResultDto> Handle(AuthenticationQuery query,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(query.Login, cancellationToken);
            var doesExist = user is not null;
            var isAuthenticated = doesExist && _encryptionService.VerifyPassword(user.Password, query.Password);
            var authenticationResult = isAuthenticated
                ? new AuthenticationResultDto(
                    true,
                    _tokenGenerationService.GenerateToken(user.Id),
                    user)
                : new AuthenticationResultDto(
                    false,
                    string.Empty,
                    default);

            return authenticationResult;
        }
    }
}
