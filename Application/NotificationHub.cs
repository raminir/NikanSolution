using Microsoft.AspNet.SignalR;

namespace Application
{
    public class NotificationHub : Hub
    {
        public void NotifyNewAppointment(string appointment)
        {
            Clients.All.broadcastNewAppointment(appointment);
        }
    }
}