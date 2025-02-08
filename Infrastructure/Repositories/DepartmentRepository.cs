using Models;
using Models.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly QueueDbContext _context;

        public DepartmentRepository(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public IList<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }

        public IList<Room> GetAllRoom()
        {
            return _context.Rooms.Include(x => x.Department).ToList();
        }

        public Department GetById(int departmentId)
        {
            return _context.Departments.Find(departmentId);
        }
    }
}
