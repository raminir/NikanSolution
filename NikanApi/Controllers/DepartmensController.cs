using Application.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace NikanApi.Controllers
{
    public class DepartmensController : ApiController
    {
        private readonly DepartmentService _departmentService;

        public DepartmensController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // POST api/<controller>
        public async Task<int> Post(string name)
        {
            var departmentId = await _departmentService.GenerateDepartmentAsync(name);
            return departmentId;
        }
    }
}