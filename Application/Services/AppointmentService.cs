using Application.Dtos;
using Microsoft.AspNet.SignalR;
using Models;

namespace Application.Services
{
    public class AppointmentService
    {
        public void CallAppointment(TicketInRoomDto model)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.All.broadcastNewAppointment(model);
        }
    }
}
