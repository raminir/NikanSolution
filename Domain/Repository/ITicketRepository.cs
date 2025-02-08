using System;
using System.Collections.Generic;

namespace Models.Repository
{
    public interface ITicketRepository
    {
        Ticket CreateTicket(Ticket ticket);
        List<TicketInRooms> GetAll();
        List<TicketInRooms> GetTodayInProgressTickets();
        List<TicketInRooms> GetAllTodayTicketByRoomId(int id);
        int GetLastTicketNumber(DateTime date, int departmentId);
        void UpdateStatus(TicketInRooms model);
        void CopyToNextRoom(int ticketId);
        StatusEnum GetStatus(int id);

        TicketInRooms GetTicketInRoomById(int id);
    }
}
