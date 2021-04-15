using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.User.Repositories;
using Core.Domain.Abstractions.BuildingBlocks;
using Core.Infrastructure.Persistence.Marten;
using User.Domain.User.Enumerations;

namespace User.Infrastructure.Persistence.Write.Repositories
{
    public sealed class UserRepository : Repository<Domain.User.User>,
        IUserRepository
    {
        public UserRepository(IEventStore eventStore)
            : base(eventStore)
        {
        }

        public new async Task<Domain.User.User> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await EventStore.FindAsync<Domain.User.User>(id, cancellationToken);

            var returnValue = user is null || user.Status == UserStatus.Deleted
                ? null
                : user;

            return returnValue;
        }
    }
}
