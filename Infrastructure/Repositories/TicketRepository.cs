using Models;
using Models.Repository;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;

namespace Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly QueueDbContext _context;

        public TicketRepository(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public List<TicketInRooms> GetAll()
        {
            return _context.TicketInRooms
                .Include(x=>x.Room)
                .Include(x=>x.Ticket)
                .ToList();
        }

        public int GetLastTicketNumber(DateTime date)
        {
            return _context.Tickets
                .Where(t => t.CreatedAt == date.Date)
                .OrderByDescending(t => t.TicketNumber)
                .Select(t => t.TicketNumber)
                .FirstOrDefault();
        }

        public async Task UpdateStatusAsync(TicketInRooms ticketInRooms)
        {
            var current = _context.TicketInRooms.FirstOrDefault(x => x.Id == ticketInRooms.Id);
            current.StatusId = ticketInRooms.StatusId;
            await _context.SaveChangesAsync();
        }
    }
}
