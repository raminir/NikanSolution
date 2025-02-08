using Application.QueueManagement.Application.Services;
using Application.ViewModels;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace NikanApi.Controllers
{
    public class TicketsController : ApiController
    {

        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET api/<controller>
        public IEnumerable<TicketInRooms> Get()
        {
            return _ticketService.GetAll();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody] int id)
        {
            var ticketNumber = _ticketService.GenerateTicketAsync(id);
            return ticketNumber;

        }

        // PUT api/<controller>/5
        public async Task<OkResult> Put(int id, [FromBody] TicketUpdateViewModel input)
        {
            _ticketService.UpdateStatus(id, input);
            return Ok();
        }
    }
}