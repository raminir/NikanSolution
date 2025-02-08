using System.Collections.Generic;

namespace Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public virtual IList<Room> Rooms { get; set; }
    }
}
