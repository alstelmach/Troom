using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistence.BuildingBlocks;

namespace User.Infrastructure.Persistence.Read.Repositories.User
{
    public interface IUserReadModelRepository : IReadModelRepository<Entities.User>
    {
        Task<Entities.User> GetAsync(string login, CancellationToken cancellationToken = default);
    }
}
