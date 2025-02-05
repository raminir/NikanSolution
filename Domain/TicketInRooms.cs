using System;
using System.Collections;

namespace Models
{
    public class TicketInRooms : BaseEntity
    {
        public int RoomId { get; set; }
        public int TicketId { get; set; }
        public StatusEnum StatusId { get; set; }
        public virtual Room Room { get; set; }
        public virtual Ticket Ticket { get; set; }

    }
}
