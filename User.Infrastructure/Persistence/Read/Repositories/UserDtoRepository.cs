using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using User.Application.Dto.Role;
using User.Application.Dto.User;
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
                : await QueryFirstOrDefaultAsync(sqlQuery);

            return user;
        }

        public async Task<UserDto> GetAsync(Guid id,
            bool includeRoles = false,
            CancellationToken cancellationToken = default)
        {
            var sqlQuery = "SELECT \"Id\", \"Login\", \"Password\", \"FirstName\", \"LastName\", \"MailAddress\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Users\" "
                + $"WHERE \"Id\" = '{id}' "
                + "LIMIT 1";

            var user = includeRoles
                ? GetUserIncludingRoles(sqlQuery)
                : await QueryFirstOrDefaultAsync(sqlQuery);

            return user;
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
