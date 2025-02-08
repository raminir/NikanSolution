using Models;

namespace Application.Dtos
{
    public class UpdateTicketStatusRequest
    {
        public StatusEnum StatusId { get; set; }
    }
}
