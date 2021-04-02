using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Queries;
using User.Application.Contracts.Authentication;
using User.Application.Dto;
using User.Domain.User.Services;
using User.Infrastructure.Persistence.Read.Repositories.User;
using User.Infrastructure.Services.TokenGeneration;

namespace User.Application.Handlers.QueryHandlers
{
    public sealed class AuthenticationQueryHandler : IQueryHandler<AuthenticationQuery, AuthenticationResultDto>
    {
        private readonly IUserReadModelRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly TokenGenerationService _tokenGenerationService;

        public AuthenticationQueryHandler(IUserReadModelRepository userRepository,
            IEncryptionService encryptionService,
            TokenGenerationService tokenGenerationService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _tokenGenerationService = tokenGenerationService;
        }
        
        public async Task<AuthenticationResultDto> Handle(AuthenticationQuery query,
            CancellationToken cancellationToken)
        {
            var authenticationResult = new AuthenticationResultDto();
            var user = await _userRepository.GetAsync(query.Login, cancellationToken);
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
                authenticationResult.TokenOwner = (UserDto) user;
            }

            return authenticationResult;
        }
    }
}
