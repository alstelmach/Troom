using Core.Infrastructure.Persistence.BuildingBlocks;
using User.Infrastructure.Persistence.Read.Context;

namespace User.Infrastructure.Persistence.Read.Repositories.User
{
    public sealed class UserReadModelRepository : ReadModelRepository<Entities.User>,
        IUserReadModelRepository
    {
        public UserReadModelRepository(UserReadModelContext dbContext) : base(dbContext)
        {
        }
    }
}
