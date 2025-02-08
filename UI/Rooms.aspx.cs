using Application.Services;
using Microsoft.Practices.Unity;
using System;

namespace UI
{
    public partial class Rooms : System.Web.UI.Page
    {
        private DepartmentService _departmentService;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Retrieve the Unity container from Application state
            var container = (IUnityContainer)Application["UnityContainer"];

            // Resolve the IUserService dependency
            _departmentService = container.Resolve<DepartmentService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var ds = _departmentService.GetAllRooms();
            rpt.DataSource = ds;
            rpt.DataBind();
        }
    }
}