using Application.Dtos;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace Application.Services
{
    public class AppointmentService
    {
        public void CallAppointment(List<TicketInRoomDto> model)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.All.broadcastNewAppointment(model);
        }
    }
}
