using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Component.User.Service
{
    public class UserService : IUserService
    {
       private readonly IUserRepository _userRepository;

        /// <summary>
        /// Defalut constructor for dependency injection
        /// </summary>
        /// <param name="userRepository">An object which implemenents an IUserRepository</param>
       public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

    }
}
