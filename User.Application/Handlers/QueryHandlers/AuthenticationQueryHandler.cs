using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Queries;
using User.Application.Contracts.Authentication;
using User.Application.Dto;
using User.Application.Dto.Repositories;
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
            var authenticationResult = new AuthenticationResultDto();
            var user = await _userRepository.GetAsync(query.Login, false, cancellationToken);
            var doesNotExist = user is null;

            if (doesNotExist)
            {
                return authenticationResult;
            }
            
            var isAuthenticated = _encryptionService.VerifyPassword(user.Password, query.Password);

            if (isAuthenticated)
            {
                authenticationResult.IsAuthenticated = true;
                authenticationResult.JsonWebToken = _tokenGenerationService.GenerateToken(user.Id);
                authenticationResult.TokenOwner = user;
            }

            return authenticationResult;
        }
    }
}
