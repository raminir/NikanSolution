using Domain.Repository;
using Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly QueueDbContext _context;

        public RoomRepository(QueueDbContext context)
        {
            _context = context;
        }

        public void Create(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }
        public IList<Room> GetAll()
        {
            return _context.Rooms.Include(x => x.Department).ToList();
        }
    }
}
