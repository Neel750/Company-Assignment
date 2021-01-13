using HMS.BAL;
using HMS.BAL.Interface;
using HMS.DAL.Repository;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace WebApiAssignment
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IHotelManager, HotelManager>();
            container.RegisterType<IRoomManager, RoomManager>();
            container.RegisterType<IBookingManager, BookingManager>();
            container.RegisterType<IHotelRepository, HotelRepository>();
            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IBookingRepository, BookingRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}