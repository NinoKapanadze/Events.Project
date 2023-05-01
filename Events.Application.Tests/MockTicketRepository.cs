using Events.Application.Tickets;
using Events.Domain;

namespace Events.Application.Tests
{
    public class MockTicketRepository : ITicketRepository
    {
        public Task BuyTicket(CancellationToken cancellationToken, string ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTicket(CancellationToken cancellationToken, string userId, EvenT myEvent)
        {
            throw new NotImplementedException();
        }

        public Task Delete(CancellationToken cancellationToken, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAll(CancellationToken cancellationToken, List<Ticket> ticket)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllAsyncForWorker(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}