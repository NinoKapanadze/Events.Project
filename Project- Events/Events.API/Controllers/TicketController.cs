using Events.Application.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketController(ITicketService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("Reserve Ticket")]
        [HttpPost]
        public async Task<string> GetTicket(string eventName, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst("UserId")!.Value;

            await _service.CreateAsync(cancellationToken, eventName, userId);
            var TicketId = "mystirng";
            return TicketId;
        }

        [Route("Buy")]
        [HttpPost]
        public async Task BuyTicket(CancellationToken cancellationToken, int id)
        {
            await _service.BuyAsync(cancellationToken, id);
        }

        [HttpGet]
        public async Task<List<ViewTicketModel>> GetMyTickets(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst("UserId")!.Value;
            var a = await _service.SeeTickets(cancellationToken, userId);
            return a;
        }
    }
}