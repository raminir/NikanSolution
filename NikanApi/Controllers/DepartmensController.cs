using Application.Services;
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
        public int Post(string name)
        {
            var departmentId = _departmentService.Create(name);
            return departmentId;
        }
    }
}