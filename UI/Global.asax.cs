using Application.QueueManagement.Application.Services;
using Application.Services;
using DAL.Repositories;
using Domain.Repository;
using Infrastructure.Repositories;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Models.Repository;
using Owin;
using System;
using System.Configuration;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
[assembly: OwinStartup(typeof(UI.Global))]
namespace UI
{
    public class Global : HttpApplication
    {
        public static IUnityContainer Container { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Initialize the Unity container
            Container = new UnityContainer();

            // Register dependencies
            Container.RegisterType<TicketService>(new HierarchicalLifetimeManager());
            //Container.RegisterType<ITicketRepository, TicketRepository>(new HierarchicalLifetimeManager());
            var connectionString = ConfigurationManager.ConnectionStrings["QueueManagementConnectionString"].ConnectionString;
            Container.RegisterType<ITicketRepository, AdoTicketRepository>(new HierarchicalLifetimeManager(), new InjectionConstructor(connectionString));
            Container.RegisterType<DepartmentService>();
            Container.RegisterType<IDepartmentRepository, DepartmentRepository>(new HierarchicalLifetimeManager());
            Container.RegisterType<IRoomRepository, RoomRepository>(new HierarchicalLifetimeManager());

            // Store the container in Application state
            Application["UnityContainer"] = Container;
        }
    }
}