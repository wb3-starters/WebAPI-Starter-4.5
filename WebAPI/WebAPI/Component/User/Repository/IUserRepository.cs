using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core.Repository;

namespace WebAPI.Component.User
{

    public interface IUserRepository : IRepository<Components.User.User>
    {
        Components.User.User GetById(long id);
    }
}
