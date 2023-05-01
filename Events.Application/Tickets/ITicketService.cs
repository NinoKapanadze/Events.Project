using Events.Domain;

namespace Events.Application.Tickets
{
    public interface ITicketService
    {
        public Task<string> CreateAsync(CancellationToken cancellationToken, string eventName, string userId);

        Task BuyAsync(CancellationToken cancellationToken, int ticketId);

        Task<List<ViewTicketModel>> SeeTickets(CancellationToken cancellationToken, string ticketId);

        Task<List<ViewTicketModel>> BuyAsyncForMvc(CancellationToken cancellationToken, string userId);

        Task<List<EvenT>> RemoveDueTickets(CancellationToken cancellationToken);
        Task<List<ViewTicketModel>> GetAllBoughtAsync(CancellationToken cancellationToken, string userId);
    }
}