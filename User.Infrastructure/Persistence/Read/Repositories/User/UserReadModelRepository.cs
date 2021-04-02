using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;
using User.Infrastructure.Persistence.Read.Context;

namespace User.Infrastructure.Persistence.Read.Repositories.User
{
    public sealed class UserReadModelRepository : ReadModelRepository<Entities.User>,
        IUserReadModelRepository
    {
        public UserReadModelRepository(UserReadModelContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Entities.User> GetAsync(string login, CancellationToken cancellationToken = default)
        {
            var sqlQuery =
                "SELECT \"Id\", \"Login\", \"Password\", \"FirstName\", \"LastName\", \"MailAddress\" "
                + $"FROM \"{UserReadModelContext.SchemaName}\".\"Users\" "
                + $"WHERE \"Login\" = '{login}' "
                + "LIMIT 1";

            var user = await QueryFirstOrDefaultAsync(sqlQuery);

            return user;
        }
    }
}
