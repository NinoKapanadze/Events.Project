using Events.Application.Eventz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventController(IEventService eventService, IHttpContextAccessor httpContextAccessor)
        {
            _eventService = eventService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> SeeAllEvents(CancellationToken cancellation = default)
        {
            var events = await _eventService.GetAllAsyncDeepLook(cancellation);
            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CancellationToken cancellationToken, EventRequestModel request)
        {
            // var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            await _eventService.CreateForMvc(cancellationToken, request);

            return View();
        }

        public async Task<IActionResult> CreateEvent(CancellationToken cancellationToken)
        {
            // var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //await _eventService.CreateForMvc(cancellationToken, request);

            return View();
        }

        public async Task<IActionResult> SeePendingEvents(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var eventss = await _eventService.SeePendingEvents(cancellationToken, userId);
            return View(eventss);
        }

        public async Task<IActionResult> ApproveEvent(CancellationToken cancellationToken, int eventId)
        {
            await _eventService.ApproveEvent(cancellationToken, eventId);
            return RedirectToAction("SeePendingEvents");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}