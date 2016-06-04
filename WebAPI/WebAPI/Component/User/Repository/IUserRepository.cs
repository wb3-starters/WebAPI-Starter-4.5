using WebAPI.Core.Repository;

namespace WebAPI.Component.User.Repository
{

    public interface IUserRepository : IRepository<Component.User.User>
    {
        Component.User.User GetById(long id);
    }
}
