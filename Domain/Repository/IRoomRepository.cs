using Models;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IRoomRepository
    {
        void Create(Room room);
        IList<Room> GetAllRoom();
    }
}
