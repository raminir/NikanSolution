using Application.Dtos;
using Application;
using System.Collections.Generic;
using System.Web.Http;

namespace NikanApi.Controllers
{
    public class TicketsController : ApiController
    {

        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }


        // POST api/<controller>
        public int Post([FromBody] int departmentId)
        {
            var ticketNumber = _ticketService.GenerateTicket(departmentId);
            return ticketNumber;

        }

        [HttpGet]
        [Route("GetTodayInProgressTickets")]
        public IEnumerable<TicketInRoomDto> GetTodayInProgressTickets()
        {
            return _ticketService.GetTodayInProgressTickets();
        }

        [HttpGet]
        [Route("byroom/{roomId}")]
        public IEnumerable<TicketInRoomDto> GetTicketsByRoomId(int roomId)
        {
            return _ticketService.GetAllByRoomId(roomId);
        }

        [HttpPatch]
        [Route("{id}/updatestatus")]
        public IHttpActionResult UpdateStatus(int id, [FromBody] UpdateTicketStatusRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body is null.");
            }
            if (request.StatusId == Models.StatusEnum.Done)
            {
                _ticketService.UpdateTicketToDone(id, request);
            }
            else
            {
                _ticketService.UpdateStatus(id, request);
            }
            return Ok();
        }
    }
}