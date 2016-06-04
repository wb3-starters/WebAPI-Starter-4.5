using System;
using Microsoft.Practices.Unity;
using WebAPI.Component.User.Service;
using WebAPI.Core;
using System.Diagnostics;
using WebAPI.Component.User.Repository;

namespace WebAPI.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            Debug.WriteLine("Registering Unity Container Types --- START");

            container
                .RegisterType<WebAPIDbContext, WebAPIDbContext>(new TransientLifetimeManager())
                .RegisterType<IUnitOfWork, UnitOfWork>(
                    new InjectionConstructor(new ResolvedParameter(typeof(WebAPIDbContext))))

                // User components registery
                .RegisterType<IUserRepository, UserRepository>(
                    new InjectionConstructor(new ResolvedParameter(typeof(WebAPIDbContext))))
                .RegisterType<IUserService, UserService>(
                    new InjectionConstructor(
                        new ResolvedParameter(typeof(IUnitOfWork)),
                        new ResolvedParameter(typeof(IUserRepository))));

            Debug.WriteLine("Registering Unity Container Types --- FINISH");
        }
    }
}
