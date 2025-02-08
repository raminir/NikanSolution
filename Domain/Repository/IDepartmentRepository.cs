using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department> Create(Department department);
        IList<Department> GetAll();
        Department GetById(int departmentId);
    }
}
