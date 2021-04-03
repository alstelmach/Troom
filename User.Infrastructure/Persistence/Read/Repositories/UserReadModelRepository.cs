using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;
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

        public async Task<UserDto> GetAsync(string login, CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT \"Id\", \"Login\", \"Password\", \"FirstName\", \"LastName\", \"MailAddress\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Users\" "
                + $"WHERE \"Login\" = '{login}' "
                + "LIMIT 1";

            var user = await QueryFirstOrDefaultAsync(sqlQuery);

            return user;
        }

        public async Task<UserDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT \"Id\", \"Login\", \"Password\", \"FirstName\", \"LastName\", \"MailAddress\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Users\" "
                + $"WHERE \"Id\" = '{id}' "
                + "LIMIT 1";

            var user = await QueryFirstOrDefaultAsync(sqlQuery);

            return user;
        }
    }
}
