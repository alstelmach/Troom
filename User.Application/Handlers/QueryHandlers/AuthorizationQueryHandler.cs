using System.Threading;
using System.Threading.Tasks;
using AsCore.Application.Abstractions.Identity;
using AsCore.Application.Abstractions.Messaging.Queries;
using User.Application.Contracts.Authorization;
using User.Application.Dto;
using User.Application.Repositories;

namespace User.Application.Handlers.QueryHandlers
{
    public sealed class AuthorizationQueryHandler : IQueryHandler<AuthorizeQuery, AuthorizationResultDto>
    {
        private readonly IUserDtoRepository _userDtoRepository;

        public AuthorizationQueryHandler(IUserDtoRepository userDtoRepository)
        {
            _userDtoRepository = userDtoRepository;
        }
        
        public async Task<AuthorizationResultDto> Handle(AuthorizeQuery query,
            CancellationToken cancellationToken)
        {
            var ownerId = query.ClaimsPrincipal.GetOwnerId();
            var isAuthorized = await _userDtoRepository.CheckIsAuthorized(
                ownerId,
                query.PermissionId,
                cancellationToken);

            var authorizationResults = new AuthorizationResultDto(
                ownerId,
                query.PermissionId,
                isAuthorized);

            return authorizationResults;
        }
    }
}
