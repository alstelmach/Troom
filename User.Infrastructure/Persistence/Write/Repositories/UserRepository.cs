using System;
using System.Collections.Generic;
using System.Linq;
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

        public new async Task<IEnumerable<Domain.User.User>> GetAsync(CancellationToken cancellationToken) =>
            (await EventStore
                .GetAsync<Domain.User.User>(cancellationToken))
                .Where(user =>
                    user.Status != UserStatus.Deleted);
    }
}
