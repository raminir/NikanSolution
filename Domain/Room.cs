using System.Collections.Generic;

namespace Models
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual IList<TicketInRooms> TicketsInRooms { get; set; }

    }
}
