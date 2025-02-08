using Models;
using System;

namespace Application.Dtos
{
    public class TicketInRoomDto
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string DepartmentName { get; set; }
        public int TicketNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public StatusEnum StatusId { get; set; }
    }
}