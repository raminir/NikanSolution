using Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly QueueDbContext _context;

        public TicketRepository(QueueDbContext context)
        {
            _context = context;
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            try
            {
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ticket;
        }

        public List<TicketInRooms> GetAll()
        {
            return _context.TicketInRooms
                .Include(x=>x.Room)
                .Include(x=>x.Ticket)
                .ToList();
        }

        public int GetLastTicketNumber(DateTime date, int departmentId)
        {
            return _context.Tickets
                .Where(t => t.CreatedAt == date.Date)
                .Where(t => t.TicketInRooms.Any(x => x.Room.DepartmentId == departmentId))
                .OrderByDescending(t => t.TicketNumber)
                .Select(t => t.TicketNumber)
                .FirstOrDefault();
        }

        public void UpdateStatus(TicketInRooms ticketInRooms)
        {
            TicketInRooms current = _context.TicketInRooms.FirstOrDefault(x => x.Id == ticketInRooms.Id);
            current.StatusId = ticketInRooms.StatusId;
            await _context.SaveChangesAsync();
        }
    }
}
