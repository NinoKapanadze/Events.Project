using Events.Application.Eventz.Models;
using Events.Domain;

namespace Events.Application.Eventz
{
    public interface IEventService
    {
        Task<string> CreateAsyncWithTitle(CancellationToken cancellationToken, EventRequestModel request, string userId);

        Task<List<EventDeepLookModel>> SeePendingEventsForMvc(CancellationToken cancellationToken);

        public Task<List<EventFirstLookModel>> GetAllAsync(CancellationToken cancellationToken);

        public Task<List<EventDeepLookModel>> GetAllAsyncDeepLook(CancellationToken cancellationToken);

        public Task CreateAsync(CancellationToken cancellationToken, EventRequestModel request, string userId);

        Task<EvenT> GetEvent(CancellationToken cancellationToken, int eventId);

        Task ArchiveAsync(CancellationToken cancellationToken, EvenT eventt);

        Task UpdateAsync(CancellationToken cancellationToken, EvenT eventt);

        Task<List<EventDeepLookModel>> SeePendingEvents(CancellationToken cancellationToken, string userId);

        Task ApproveEvent(CancellationToken cancellationToken, int eventId);

        Task CreateForMvc(CancellationToken cancellationToken, EventRequestModel request);

        Task ApproveEventForMvc(CancellationToken cancellationToken, EventDeepLookModel e);
    }
}