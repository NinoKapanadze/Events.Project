using Events.Domain;

namespace Events.Application.Tickets
{
    public interface ITicketRepository
    {
        Task<string> CreateTicket(CancellationToken cancellationToken, string userId, EvenT myEvent);

        Task BuyTicket(CancellationToken cancellationToken, int ticketId);

        Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken, string userId);

        Task<List<Ticket>> GetAllAsyncForWorker(CancellationToken cancellationToken);

        Task Delete(CancellationToken cancellationToken, Ticket ticket);

        Task DeleteAll(CancellationToken cancellationToken, List<Ticket> ticket);
        Task<List<Ticket>> GetAllBoughtAsync(CancellationToken cancellationToken, string userId);
    }
}