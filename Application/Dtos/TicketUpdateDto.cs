using Models;

namespace Application.Dtos
{
    public class TicketUpdateDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int TicketId { get; set; }
        public StatusEnum StatusId { get; set; }
    }
}
