using Application.ViewModels;
using Microsoft.AspNet.SignalR;
using Models;

namespace Application.Services
{
    public class AppointmentService
    {
        public void CallAppointment(TicketInRoomViewModel model)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.All.broadcastNewAppointment(model);
        }
    }
}
