using Application.Services;
using Microsoft.Practices.Unity;
using System;
using System.Web.UI;

namespace UI
{
    public partial class SiteMaster : MasterPage
    {
        private RoomService _roomService;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Retrieve the Unity container from Application state
            var container = (IUnityContainer)Application["UnityContainer"];

            // Resolve the IUserService dependency
            _roomService = container.Resolve<RoomService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var ds = _roomService.GetAllRooms();
            rptMenu.DataSource = ds;
            rptMenu.DataBind();
        }
    }
}