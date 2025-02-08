using Models;
using Models.Repository;
using System.Collections.Generic;

namespace Application.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public int Create(string name)
        {
            var department = new Department
            {
                Name = name
            };

            _departmentRepository.Create(department);
            return department.Id;
        }

        public IList<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAll();
        }

    }
}
