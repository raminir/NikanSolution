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

            // Resolve the IUserService dependency
            _ticketService = container.Resolve<TicketService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var ds = _ticketService.GetAll();
            rpt.DataSource = ds;
            rpt.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var ticketNumber = _ticketService.GenerateTicketAsync(1);
            Label1.Text = ticketNumber.ToString();
        }
    }
}