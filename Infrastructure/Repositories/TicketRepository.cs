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
                .Include(x => x.Room.Department)
                .Include(x => x.Ticket)
                .ToList();
        }

        public List<TicketInRooms> GetTodayTicketsForBoard()
        {
            var today = DateTime.Now.Date;
            return _context.TicketInRooms
                .Include(x => x.Room.Department)
                .Include(x => x.Ticket)
                .OrderByDescending(x => x.CalledAt)
                .Where(x => x.Ticket.CreatedAt == today)
                .Where(x => x.StatusId == StatusEnum.InProgress)
                .ToList();
        }

        public IList<TicketInRooms> GetAllTodayTicketByRoomId(int id)
        {

            var today = DateTime.Now.Date;

            return _context.TicketInRooms
                .Include(x => x.Room.Department)
                .Include(x => x.Ticket)
                .Where(x => x.RoomId == id)
                .Where(x => x.Ticket.CreatedAt == today)
                .OrderBy(x => x.StatusId)
                .ThenByDescending(x => x.CalledAt)
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

            current.CalledAt = DateTime.Now;

            _context.SaveChanges();
        }

        public void CopyToNextRoom(int ticketInRooms)
        {
            QueueDbContext databaseContext = null;
            try
            {
                databaseContext =
                    new QueueDbContext();

                TicketInRooms current = databaseContext.TicketInRooms.Include(x => x.Room).FirstOrDefault(x => x.Id == ticketInRooms);
                var currentDepartmentId = current.Room.DepartmentId;
                var nextRoom = databaseContext.Rooms.Where(x => x.DepartmentId == currentDepartmentId).FirstOrDefault(x => x.Id > current.RoomId);
                if (nextRoom != null)
                {
                    current.RoomId = nextRoom.Id;
                    current.CalledAt = DateTime.Now;
                    current.StatusId = StatusEnum.Waiting;
                    databaseContext.TicketInRooms.Add(current);
                    databaseContext.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (databaseContext != null)
                {
                    databaseContext.Dispose();
                    databaseContext = null;
                }
            }
        }

        public StatusEnum GetStatus(int id)
        {
            return _context.TicketInRooms.Find(id).StatusId;
        }

        public TicketInRooms GetTicketInRoomById(int id)
        {
            return _context.TicketInRooms.Find(id);
        }
    }
}
