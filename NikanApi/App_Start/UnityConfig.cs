using Application;
using Application.Services;
using Infrastructure.Repositories;
using Models.Repository;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
namespace NikanApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<TicketService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITicketRepository, TicketRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<DepartmentService>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}