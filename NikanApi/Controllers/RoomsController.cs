using Application.Services;
using Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace NikanApi.Controllers
{
    public class RoomsController : ApiController
    {
        private readonly DepartmentService _departmentService;

        public RoomsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }



        // POST api/<controller>
        public async Task<int>  Post(int departmentId , string name )
        {
            var roomId = await _departmentService.GenerateRoomAsync(new Room() { DepartmentId = departmentId,Name = name});
            return roomId;
        }
    }
}