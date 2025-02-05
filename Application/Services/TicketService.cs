using Application.ViewModels;
using Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    namespace QueueManagement.Application.Services
    {
        public class TicketService
        {
            private readonly ITicketRepository _ticketRepository;

            public TicketService(ITicketRepository ticketRepository)
            {
                _ticketRepository = ticketRepository;
            }
            public int GetNextTicketNumber(DateTime date)
            {
                int lastNumber = _ticketRepository.GetLastTicketNumber(date);
                return lastNumber + 1;
            }

            public int GenerateTicketAsync(int roomId)
            {
                int newTicketNumber = GetNextTicketNumber(DateTime.Now.Date);

                var ticket = new Ticket
                {
                    TicketNumber = newTicketNumber,
                    CreatedAt = DateTime.Now.Date,
                };
                ticket.TicketInRooms = new List<TicketInRooms> {
                    new TicketInRooms()
                    {
                        RoomId = roomId,
                        StatusId = StatusEnum.Waiting
                    }
                };
                _ticketRepository.CreateTicketAsync(ticket);
                return ticket.TicketNumber;
            }

            public async Task UpdateStatusAsyncUpdate(int id, TicketUpdateViewModel input)
            {
                var model = new TicketInRooms()
                {
                    Id = id,
                    StatusId = input.StatusId,
                };
                await _ticketRepository.UpdateStatusAsync(model);
            }

            public IList<TicketInRooms> GetAll()
            {
                return _ticketRepository.GetAll();
            }
        }
    }
}
