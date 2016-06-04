using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPI.Component.User.Service
{
    public interface IUserService :IService<Component.User.User>
    {
        Component.User.User FindById(int id);
    }
}
