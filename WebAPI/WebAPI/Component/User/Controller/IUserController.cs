using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Component.User.Controller
{
    public interface IUserController
    {
        HttpResponseMessage UserById(int id, HttpRequestMessage request);
    }
}
