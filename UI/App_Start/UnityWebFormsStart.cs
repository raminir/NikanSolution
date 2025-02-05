using Application.QueueManagement.Application.Services;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.Practices.Unity;
using Models.Repository;
using System.Web;
using Unity.WebForms;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(UI.App_Start.UnityWebFormsStart), "PostStart")]
namespace UI.App_Start
{
    /// <summary>
    ///		Startup class for the Unity.WebForms NuGet package.
    /// </summary>
    internal static class UnityWebFormsStart
    {
        /// <summary>
        ///     Initializes the unity container when the application starts up.
        /// </summary>
        /// <remarks>
        ///		Do not edit this method. Perform any modifications in the
        ///		<see cref="RegisterDependencies" /> method.
        /// </remarks>
        internal static void PostStart()
        {
            IUnityContainer container = new UnityContainer();
            HttpContext.Current.Application.SetContainer(container);
            container.RegisterType<TicketService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITicketRepository, TicketRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<DepartmentService>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>(new HierarchicalLifetimeManager());

            RegisterDependencies(container);
        }

        /// <summary>
        ///		Registers dependencies in the supplied container.
        /// </summary>
        /// <param name="container">Instance of the container to populate.</param>
        private static void RegisterDependencies(IUnityContainer container)
        {
            // TODO: Add any dependencies needed here
        }
    }
}