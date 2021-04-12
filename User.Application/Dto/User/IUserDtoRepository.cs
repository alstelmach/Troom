using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;

namespace User.Application.Dto.User
{
    public interface IUserDtoRepository : IReadModelRepository<UserDto>
    {
        Task<UserDto> GetAsync(string login, bool includeRoles = false, CancellationToken cancellationToken = default);
        Task<UserDto> GetAsync(Guid id, bool includeRoles = false, CancellationToken cancellationToken = default);
    }
}
