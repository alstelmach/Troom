using System.Threading;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Queries;
using User.Application.Contracts.User.Queries;
using User.Application.Dto.User;

namespace User.Application.Handlers.QueryHandlers
{
    public sealed class UserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly IUserDtoRepository _userDtoRepository;

        public UserQueryHandler(IUserDtoRepository userDtoRepository)
        {
            _userDtoRepository = userDtoRepository;
        }

        public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken) =>
            await _userDtoRepository
                .GetAsync(query.Id, false, cancellationToken);
    }
}
