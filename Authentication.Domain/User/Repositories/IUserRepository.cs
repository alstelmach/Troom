using Core.Domain.Abstractions.BuildingBlocks;

namespace Authentication.Domain.User.Repositories
{
    public interface IUserRepository : ICrudRepository<User>
    {
    }
}
