using Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<int> GenerateDepartmentAsync(string name)
        {
            var department = new Department
            {
                Name = name
            };

            await _departmentRepository.CreateDepartmentAsync(department);
            return department.Id;
        }
        public async Task<int> GenerateRoomAsync(Room room)
        {
            await _departmentRepository.CreateRoomAsync(room);
            return room.Id;
        }

        public IList<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAllDepartments();
        }

        public IList<Room> GetAllRooms()
        {
            return _departmentRepository.GetAllRoom();
        }
    }
}
