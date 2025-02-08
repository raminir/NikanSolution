using Domain.Repository;
using Models;
using Models.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly QueueDbContext _context;

        public DepartmentRepository(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<Department> Create(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public IList<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int departmentId)
        {
            return _context.Departments.Include(x => x.Rooms).FirstOrDefault(x=>x.Id == departmentId);
        }
    }
}
