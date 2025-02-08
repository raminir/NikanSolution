using Application.QueueManagement.Application.Services;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.Practices.Unity;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;
using Domain.Repository;
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
            Container.RegisterType<ITicketRepository, TicketRepository>(new HierarchicalLifetimeManager());
            Container.RegisterType<DepartmentService>();
            Container.RegisterType<IDepartmentRepository, DepartmentRepository>(new HierarchicalLifetimeManager());
            Container.RegisterType<IRoomRepository, RoomRepository>(new HierarchicalLifetimeManager());

            // Store the container in Application state
            Application["UnityContainer"] = Container;
        }
    }
}