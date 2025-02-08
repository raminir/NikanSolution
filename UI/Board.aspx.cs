using Application.QueueManagement.Application.Services;
using Microsoft.Ajax.Utilities;
using Microsoft.Practices.Unity;
using System;

namespace UI
{
    public partial class Board : System.Web.UI.Page
    {
        private TicketService _ticketService;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Retrieve the Unity container from Application state
            var container = (IUnityContainer)Application["UnityContainer"];

            // Resolve the IUserService dependency
            _ticketService = container.Resolve<TicketService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var roomId = Request.QueryString["Id"];

            var ds = _ticketService.GetTodayTicketsForBoard();
            rpt.DataSource = ds;
            rpt.DataBind();
        }
    }
}