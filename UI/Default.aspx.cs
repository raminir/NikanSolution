using Application.QueueManagement.Application.Services;
using Microsoft.Practices.Unity;
using System;
using System.Web.UI;

namespace UI
{
    public partial class _Default : Page
    {
        private TicketService _ticketService;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Retrieve the Unity container from Application state
            var container = (IUnityContainer)Application["UnityContainer"];

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