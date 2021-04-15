using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;

namespace User.Application.Dto.Repositories
{
    public interface IRoleDtoRepository : IReadModelRepository<RoleDto>
    {
        Task CreatePermissionAssignmentAsync(Guid permissionId, Guid roleId);
        Task<RoleDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
