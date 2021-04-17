using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsCore.Infrastructure.Persistence.BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using User.Application.Dto;
using User.Application.Repositories;
using User.Infrastructure.Persistence.Read.Context;
using User.Infrastructure.Persistence.Read.Entities;

namespace User.Infrastructure.Persistence.Read.Repositories
{
    public sealed class RoleDtoRepository : ReadModelRepository<RoleDto>,
        IRoleDtoRepository
    {
        public RoleDtoRepository(UserReadModelContext dbContext)
            : base(dbContext)
        {
        }

        public async Task CreatePermissionAssignmentAsync(Guid permissionId,
            Guid roleId)
        {
            var assignment = new RolePermissionAssignment(roleId, permissionId);

            await DbContext
                .Set<RolePermissionAssignment>()
                .AddAsync(assignment);

            await DbContext.SaveChangesAsync();
            DbContext.Entry(assignment).State = EntityState.Detached;
        }

        public async Task<RoleDto> GetAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT \"Id\", \"Name\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Roles\" "
                + $"WHERE \"Id\" = '{id}' "
                + "LIMIT 1";

            var user = await QueryFirstOrDefaultAsync(sqlQuery, cancellationToken);

            return user;
        }

        public async Task<IEnumerable<RoleDto>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT roles.\"Id\", roles.\"Name\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Roles\" roles "
                + $"INNER JOIN \"{UserReadModelContext.SchemaName}\".\"UserRoleAssignments\" assignments ON roles.\"Id\" = assignments.\"RolesId\" "
                + $"WHERE assignments.\"UsersId\" = '{userId}'";

            var roles = await QueryAsync(sqlQuery, cancellationToken);

            return roles;
        }
    }
}
