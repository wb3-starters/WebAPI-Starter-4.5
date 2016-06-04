using System.Data.Entity;
using System.Linq;
using WebAPI.Core.Repository;

namespace WebAPI.Component.User.Repository
{

    public class UserRepository : Repository<Component.User.User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public Component.User.User GetById(long id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
