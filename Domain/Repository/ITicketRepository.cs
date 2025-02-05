using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Repository
{
    public interface ITicketRepository
    {
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        List<TicketInRooms> GetAll();
        int GetLastTicketNumber(DateTime date);
        Task UpdateStatusAsync(TicketInRooms model);
    }
}
