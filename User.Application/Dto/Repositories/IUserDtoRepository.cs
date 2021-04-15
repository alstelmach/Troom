using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;

namespace User.Application.Dto.Repositories
{
    public interface IUserDtoRepository : IReadModelRepository<UserDto>
    {
        Task<IEnumerable<UserDto>> GetAsync(CancellationToken cancellationToken = default);
        Task<UserDto> GetAsync(string login, bool includeRoles = false, CancellationToken cancellationToken = default);
        Task<UserDto> GetAsync(Guid id, bool includeRoles = false, CancellationToken cancellationToken = default);
        Task<bool> CheckIsAuthorized(Guid userId, Guid permissionId, CancellationToken cancellationToken = default);
    }
}
