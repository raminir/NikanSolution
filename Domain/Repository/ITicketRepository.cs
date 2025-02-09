using System.Collections.Generic;

namespace Models.Repository
{
    public interface ITicketRepository
    {
        int GetLastTicketNumberByDepartmentIdForToday(int departmentId);
        List<TicketInRooms> GetAllTodayTicketByRoomId(int id);
        List<TicketInRooms> GetTicketsInProgressForToday();
        TicketInRooms GetTicketInRoomById(int id);
        void UpdateStatus(TicketInRooms model);
        Ticket CreateTicket(Ticket ticket);
        void CreateTicketInRoom(TicketInRooms ticket);
        void CopyToNextRoom(int ticketId);
        StatusEnum GetStatus(int id);
        void CreateTicketWithTicketInRoom(Ticket ticket);
    }
}
