using Application.Dtos;
using Application.Services;
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
            private readonly AppointmentService _appointmentService;
            public TicketService(ITicketRepository ticketRepository, IDepartmentRepository departmentRepository, AppointmentService appointmentService)
            {
                _ticketRepository = ticketRepository;
                _departmentRepository = departmentRepository;
                _appointmentService = appointmentService;
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
                int newTicketNumber = GetNextTicketNumber(DateTime.Now.Date, departmentId);

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

            public IList<TicketInRoomDto> GetAllByRoomId(int Id)
            {
                var tickets = _ticketRepository.GetAllTodayTicketByRoomId(Id);
                var resutl = new List<TicketInRoomDto>();

                tickets.ForEach(ticket =>
                {
                    resutl.Add(new TicketInRoomDto
                    {
                        Id = ticket.Id,
                        DepartmentName = ticket.Room.Department.Name,
                        RoomName = ticket.Room.Name,
                        TicketNumber = ticket.Ticket.TicketNumber,
                        CreatedAt = ticket.Ticket.CreatedAt,
                        StatusId = ticket.StatusId,
                    });
                });
                return resutl;
            }

            public IList<TicketInRoomDto> GetTodayInProgressTickets()
            {
                var tickets = _ticketRepository.GetTicketsInProgressForToday();
                var resutl = new List<TicketInRoomDto>();

                tickets.ForEach(ticket =>
                {
                    resutl.Add(new TicketInRoomDto { RoomName = ticket.Room.Name, TicketNumber = ticket.Ticket.TicketNumber });
                });

                return resutl;
            }

            public void UpdateTicketToDone(int id, UpdateTicketStatusRequest input)
            {
                UpdateStatus(id, input);
                _ticketRepository.CopyToNextRoom(id);
            }
            public void UpdateStatus(int id, UpdateTicketStatusRequest input)
            {
                var currentStatus = _ticketRepository.GetStatus(id);

                if (currentStatus != StatusEnum.InProgress && (input.StatusId == StatusEnum.Done || input.StatusId == StatusEnum.cancel))
                {
                    throw new InvalidOperationException("درصورتیکه که میخواهید انجام شده یا عدم مراجعه را بزنید باید وضعیت در حالت فراخوان  باشد.");
                }

                var model = new TicketInRooms()
                {
                    Id = id,
                    StatusId = input.StatusId,
                };
                _ticketRepository.UpdateStatus(model);
                if (model.StatusId == StatusEnum.InProgress)
                {
                    SendModelToHub(model.Id);
                }
            }

            public void SendModelToHub(int TicketInRoomId)
            {
                var ticketInRooms = _ticketRepository.GetTicketInRoomById(TicketInRoomId);
                _appointmentService.CallAppointment(new TicketInRoomDto() { RoomName = ticketInRooms.Room.Name, TicketNumber = ticketInRooms.Ticket.TicketNumber });

            }
        }
    }
}
