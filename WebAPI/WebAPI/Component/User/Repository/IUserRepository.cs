using WebAPI.Core.Repository;

namespace WebAPI.Component.User.Repository
{

    public interface IUserRepository : IRepository<Components.User.User>
    {
        Components.User.User GetById(long id);
    }
}
