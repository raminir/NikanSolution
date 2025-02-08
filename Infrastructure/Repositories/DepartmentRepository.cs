using Models;
using Models.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly QueueDbContext _context;

        public DepartmentRepository(QueueDbContext context)
        {
            _context = context;
        }
        public Department Create(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public IList<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int departmentId)
        {
            return _context.Departments.Include(x => x.Rooms).FirstOrDefault(x => x.Id == departmentId);
        }
    }
}
