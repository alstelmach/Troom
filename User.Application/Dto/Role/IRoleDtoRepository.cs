using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;

namespace User.Application.Dto.Role
{
    public interface IRoleDtoRepository : IReadModelRepository<RoleDto>
    {
        Task CreatePermissionAssignmentAsync(Guid permissionId, Guid roleId, CancellationToken cancellationToken);
        Task<RoleDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
