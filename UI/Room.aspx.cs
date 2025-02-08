using Application.QueueManagement.Application.Services;
using Application.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.Practices.Unity;
using Models;
using System;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class Room : System.Web.UI.Page
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
            if (!IsPostBack)  
            {
                LoadTickets();
            }
        }

        protected void StatusButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var ticketId = int.Parse(btn.CommandArgument);
            switch (int.Parse(btn.CommandName))
            {
                case (int)Models.StatusEnum.Waiting:
                    _ticketService.UpdateStatusAsyncUpdate(ticketId, new TicketUpdateViewModel() { StatusId = Models.StatusEnum.Waiting });
                    break;
                case (int)Models.StatusEnum.InProgress:
                    _ticketService.UpdateStatusAsyncUpdate(ticketId, new TicketUpdateViewModel() { StatusId = Models.StatusEnum.InProgress });
                    break;
                case (int)Models.StatusEnum.Done:
                    _ticketService.UpdateStatusAsyncUpdate(ticketId, new TicketUpdateViewModel() { StatusId = Models.StatusEnum.Done });
                    break;
                case (int)Models.StatusEnum.cancel:
                    _ticketService.UpdateStatusAsyncUpdate(ticketId, new TicketUpdateViewModel() { StatusId = Models.StatusEnum.cancel });
                    break;
                    //case "ThatBtnClick":
                    //    //DoSomethingElse(btn.CommandArgument.ToString());
                    //    break;
            }
            LoadTickets();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var ticketId = int.Parse(btn.CommandArgument);
            _ticketService.CopyToNextRoom(ticketId);
        }
        protected string GetStatusDisplayName(object statusId)
        {
            if (statusId == null) return "نامشخص";

            if (Enum.IsDefined(typeof(StatusEnum), statusId))
            {
                StatusEnum status = (StatusEnum)statusId;
                return status.GetDisplayName();
            }

            return "نامشخص";
        }
        private void LoadTickets()
        {
            var roomId = Request.QueryString["Id"];

            var ds = string.IsNullOrWhiteSpace(roomId)
                ? _ticketService.GetAll()
                : _ticketService.GetAllByRoomId(int.Parse(roomId));

            rpt.DataSource = ds;
            rpt.DataBind();
        }

    }
}