using Authentication.Domain.User;
using Authentication.Domain.User.Repositories;
using Core.Domain.Abstractions.BuildingBlocks;
using Core.Infrastructure.Persistence.PostgreSQL.Marten;

namespace Authentication.Infrastructure.Persistence.Repositories
{
    public sealed class UserRepository : Repository<User>,
        IUserRepository
    {
        public UserRepository(IEventStore eventStore)
            : base(eventStore)
        {
        }
    }
}
