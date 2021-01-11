using PMS.BAL;
using PMS.BAL.Interface;
using PMS.DAL.Repository;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace PMS.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProductManager, ProductManager>();
            container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}