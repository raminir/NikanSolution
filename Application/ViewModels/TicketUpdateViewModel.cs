using Models;

namespace Application.ViewModels
{
    public class TicketUpdateViewModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int TicketId { get; set; }
        public StatusEnum StatusId { get; set; }
    }
}
