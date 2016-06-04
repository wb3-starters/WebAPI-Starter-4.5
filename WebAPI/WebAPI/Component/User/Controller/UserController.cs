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
        [ResponseType(typeof(Component.User.User))]
        public HttpResponseMessage UserById(int id, HttpRequestMessage request)
        {
            try
            {
                Component.User.User user = new Component.User.User();
                HttpResponseMessage message = new HttpResponseMessage();

                user = _userService.FindById(id);

                if (user == null)
                {
                    string notFoundText = String.Format("User '{0}' does not exist", id.ToString());
                    message = request.CreateResponse(HttpStatusCode.NotFound, notFoundText);
                }
                else
                {
                    message = request.CreateResponse(HttpStatusCode.OK, user);
                }

                return message;
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.NotFound, ex.ToString());
            }

        }
    }
}
