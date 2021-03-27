using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Repositories
{
    public interface IUserRepository : ICrudRepository<User>
    {
    }
}
