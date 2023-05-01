using Events.Domain;

namespace Events.Application.Eventz
{
    public interface IEventRepository
    {
        Task<List<EvenT>> GetAllAsync(CancellationToken cancellationToken);

        Task CreateAsync(CancellationToken cancellationToken, EvenT eventt);

        Task ArchiveAsync(CancellationToken cancellationToken, EvenT eventt);

        Task<EvenT> GetEvent(CancellationToken cancellationToken, int eventId);

        Task<EvenT> GetEventForMvc(CancellationToken cancellationToken, EvenT eventId);

        Task UpdateAsync(CancellationToken cancellationToken, EvenT eventt);

        Task<List<EvenT>> GetPendingEventsAnync(CancellationToken cancellationToken);
    }
}