using User.Domain.User.Repositories;
using Core.Domain.Abstractions.BuildingBlocks;
using Core.Infrastructure.Persistence.Marten;

namespace User.Infrastructure.Persistence.Write.Repositories
{
    public sealed class UserRepository : Repository<Domain.User.User>,
        IUserRepository
    {
        public UserRepository(IEventStore eventStore)
            : base(eventStore)
        {
        }
    }
}
