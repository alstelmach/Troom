using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using User.Application.Dto;
using User.Application.Dto.Repositories;
using User.Infrastructure.Persistence.Read.Context;

namespace User.Infrastructure.Persistence.Read.Repositories
{
    public sealed class UserDtoRepository : ReadModelRepository<UserDto>,
        IUserDtoRepository
    {
        public UserDtoRepository(UserReadModelContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT \"Id\", \"Login\", \"Password\", \"FirstName\", \"LastName\", \"MailAddress\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Users\"";

            var users = await QueryAsync(sqlQuery, cancellationToken);

            return users;
        }

        public async Task<UserDto> GetAsync(string login,
            bool includeRoles = false,
            CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT \"Id\", \"Login\", \"Password\", \"FirstName\", \"LastName\", \"MailAddress\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Users\" "
                + $"WHERE \"Login\" = '{login}' "
                + "LIMIT 1";

            var user = includeRoles
                ? GetUserIncludingRoles(sqlQuery)
                : await QueryFirstOrDefaultAsync(sqlQuery, cancellationToken);

            return user;
        }

        public async Task<UserDto> GetAsync(Guid id,
            bool includeRoles = false,
            CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT \"Id\", \"Login\", \"Password\", \"FirstName\", \"LastName\", \"MailAddress\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Users\" "
                + $"WHERE \"Id\" = '{id}' "
                + "LIMIT 1";

            var user = includeRoles
                ? GetUserIncludingRoles(sqlQuery)
                : await QueryFirstOrDefaultAsync(sqlQuery, cancellationToken);

            return user;
        }

        public async Task<bool> CheckIsAuthorized(Guid userId,
            Guid permissionId,
            CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT COUNT(*) "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"PermissionRoleAssignments\" permissionRoleAssignments "
                + $"INNER JOIN \"{UserReadModelContext.SchemaName}\".\"UserRoleAssignments\" userRoleAssignments "
                + "ON permissionRoleAssignments.\"RoleId\" = userRoleAssignments.\"RolesId\" "
                + $"WHERE userRoleAssignments.\"UsersId\" = '{UserReadModelContext.SchemaName}' "
                + $"AND permissionRoleAssignments.\"PermissionId\" = '{UserReadModelContext.SchemaName}' "
                + "LIMIT 1";

            var isAuthorized = await DbContext
                .Database
                .GetDbConnection()
                .QueryFirstOrDefaultAsync<bool>(sqlQuery, cancellationToken);

            return isAuthorized;
        }

        private UserDto GetUserIncludingRoles(string sqlQuery) =>
            DbContext
                .Database
                .GetDbConnection()
                .Query<UserDto, RoleDto, UserDto>(sqlQuery,
                    (userDto, roleDto) =>
                    {
                        userDto.Roles.Add(roleDto);
                        return userDto;
                    },
                    splitOn: "RolesId")
                .FirstOrDefault();
    }
}
