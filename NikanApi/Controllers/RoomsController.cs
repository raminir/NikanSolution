using Application.Services;
using Models;
using System.Web.Http;

namespace NikanApi.Controllers
{
    public class RoomsController : ApiController
    {
        private readonly RoomService _roomService;

        public RoomsController(RoomService roomService)
        {
            _roomService = roomService;
        }





        // POST api/<controller>
        public int Post(int departmentId, string name)
        {
            var roomId = _roomService.CreateRoom(new Room() { DepartmentId = departmentId, Name = name });
            return roomId;
        }
    }
}