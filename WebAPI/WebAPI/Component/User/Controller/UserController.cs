using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Component.User.Service;
using WebAPI.Components.User;

namespace WebAPI.Component.User.Controller
{
    [RoutePrefix("api/user")]
    public class UserController : IUserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("{id}")]
        [ResponseType(typeof(Components.User.User))]
        public HttpResponseMessage UserById(int id, HttpRequestMessage request)
        {
            try
            {
                Components.User.User user = new Components.User.User();
                user = _userService.FindById(id);

                return request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch(Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.NotFound, ex.ToString());
            }
            
        }
    }
}
