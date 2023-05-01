using Events.Application.Eventz;
using Events.Application.Eventz.Models;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Events.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventController(IEventService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("See Tickets unathorized")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<EventFirstLookModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }

        [Route("authorized")]
        [HttpGet]
        public async Task<List<EventDeepLookModel>> GetAllDeepLook(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsyncDeepLook(cancellationToken);
        }

        [HttpPost]
        public async Task CreateEvent(CancellationToken cancellationToken, EventRequestModel request)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst("UserId")!.Value;

            await _service.CreateAsync(cancellationToken, request, userId);
        }

        [Route("Pending Events")]
        [HttpGet]
        public async Task<List<EventDeepLookModel>> SeePendingEvents(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst("UserId")!.Value;
            var eventss = await _service.SeePendingEvents(cancellationToken, userId);
            return eventss;
        }

        [Route("ApproveEvent")]
        [HttpPut]
        public async Task ApproveEvent(CancellationToken cancellationToken, int eventId)
        {
            _service.ApproveEvent(cancellationToken, eventId);
        }
    }
}