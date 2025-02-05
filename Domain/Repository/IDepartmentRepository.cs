using System.Threading.Tasks;

namespace Models.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateDepartmentAsync(Department department);
        Task<Room> CreateRoomAsync(Room room);
    }
}
