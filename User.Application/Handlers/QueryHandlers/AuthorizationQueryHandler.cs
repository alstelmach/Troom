using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Queries;
using Core.Infrastructure.Identity;
using User.Application.Contracts.Authorization;
using User.Application.Dto;
using User.Application.Dto.Repositories;

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

            var authorizationResults = new AuthorizationResultDto
            {
                UserId = ownerId,
                PermissionId = query.PermissionId,
                IsAuthorized = isAuthorized
            };
            

            return authorizationResults;
        }
    }
}
