using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPI.Component.User.Service
{
    public interface IUserService :IService<Components.User.User>
    {
        Components.User.User FindById(int id);
    }
}
