using Application.Services;
using Microsoft.Practices.Unity;
using System;

namespace UI
{
    public partial class Rooms : System.Web.UI.Page
    {
        private RoomService roomService;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Retrieve the Unity container from Application state
            var container = (IUnityContainer)Application["UnityContainer"];

            // Resolve the IUserService dependency
            roomService = container.Resolve<RoomService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var ds = roomService.GetAllRooms();
            rpt.DataSource = ds;
            rpt.DataBind();
        }
    }
}