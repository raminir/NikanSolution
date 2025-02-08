using Domain.Repository;
using Models;
using Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public int CreateRoom(Room room)
        {
            _roomRepository.Create(room);
            return room.Id;
        }

        public IList<Room> GetAllRooms()
        {
            return _roomRepository.GetAll();
        }
    }
}
