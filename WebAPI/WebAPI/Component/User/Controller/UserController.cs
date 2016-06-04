using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Component.User.Service;

namespace WebAPI.Component.User.Controller
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController, IUserController
    {
        IUserService _userService;

        public UserController() { }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Components.User.User))]
        public HttpResponseMessage UserById(int id, HttpRequestMessage request)
        {
            try
            {
                Components.User.User user = new Components.User.User();
                _userService.FindById(id);
                return request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch(Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.NotFound, ex.ToString());
            }
            
        }
    }
}
