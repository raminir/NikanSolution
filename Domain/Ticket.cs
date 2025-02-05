using System;
using System.Collections.Generic;

namespace Models
{
    public class Ticket : BaseEntity
    {
        public int TicketNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual IList<TicketInRooms> TicketInRooms { get; set; }
    }
}
