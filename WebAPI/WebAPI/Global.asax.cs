using System;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.App_Start;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Register Unity IoC in the Global Configuration so it can be accessed by the controllers
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(UnityConfig.GetConfiguredContainer());
        }

    }
}
