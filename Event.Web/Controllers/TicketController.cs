using System.Security.Claims;
using Events.Application.Eventz;
using Events.Application.Tickets;
using Events.Domain;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Event.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _TicketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TicketController(ITicketService eventService, IHttpContextAccessor httpContextAccessor)
        {
            _TicketService = eventService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetTicket(CancellationToken cancellationToken, string Title)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var m = await _TicketService.CreateAsync(cancellationToken, Title, userId);
            return RedirectToAction("SeeAllEvents", "Event");
            // var TicketId = "mystirng";
        }

        public async Task<IActionResult> ViewReservedTicket(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var a = await _TicketService.BuyAsyncForMvc(cancellationToken, userId);
            return View(a);
        }
        public async Task<IActionResult> ViewBoughtTicket(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var a = await _TicketService.GetAllBoughtAsync(cancellationToken, userId);
            return View(a);
        }

        public async Task<IActionResult> BuyTicket(CancellationToken cancellationToken, int id)
        {
            await _TicketService.BuyAsync(cancellationToken, id);
            return RedirectToAction("ViewReservedTicket");
        }
     
    }
}
