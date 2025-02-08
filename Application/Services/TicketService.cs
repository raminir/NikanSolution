using Application.ViewModels;
using Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    namespace QueueManagement.Application.Services
    {
        public class TicketService
        {
            private readonly ITicketRepository _ticketRepository;
            private readonly IDepartmentRepository _departmentRepository;
            public TicketService(ITicketRepository ticketRepository, IDepartmentRepository departmentRepository)
            {
                _ticketRepository = ticketRepository;
                _departmentRepository = departmentRepository;
            }
            public int GetNextTicketNumber(DateTime date, int departmentId)
            {
                int lastNumber = _ticketRepository.GetLastTicketNumber(date, departmentId);
                return lastNumber + 1;
            }

            public int GenerateTicketAsync(int departmentId)
            {
                var department = _departmentRepository.GetById(departmentId);
                var firstRoomId = department.Rooms.FirstOrDefault().Id;
                int newTicketNumber = GetNextTicketNumber(DateTime.Now.Date,departmentId);

                var ticket = new Ticket
                {
                    TicketNumber = newTicketNumber,
                    CreatedAt = DateTime.Now.Date,
                };
                ticket.TicketInRooms = new List<TicketInRooms> {
                    new TicketInRooms()
                    {
                        RoomId = firstRoomId,
                        StatusId = StatusEnum.Waiting,
                        CalledAt = DateTime.Now,
                    }
                };
                _ticketRepository.CreateTicket(ticket);
                return ticket.TicketNumber;
            }

            public void UpdateStatusAsyncUpdate(int id, TicketUpdateViewModel input)
            {
                var model = new TicketInRooms()
                {
                    Id = id,
                    StatusId = input.StatusId,
                };
                _ticketRepository.UpdateStatus(model);
            }

            public IList<TicketInRooms> GetAll()
            {
                return _ticketRepository.GetAll();
            }

            public IList<TicketInRooms> GetAllByRoomId(int Id)
            {
                return _ticketRepository.GetTodayTicketAllByRoomId(Id);
            }

            public IList<TicketInRooms> GetTodayTicketsForBoard()
            {
                return _ticketRepository.GetTodayTicketsForBoard();
            }

            public void CopyToNextRoom(int ticketId)
            {
                _ticketRepository.CopyToNextRoom(ticketId);
            }
        }
    }
}
