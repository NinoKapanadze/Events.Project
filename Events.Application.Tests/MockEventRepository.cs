using Events.Application.Eventz;
using Events.Domain;

namespace Events.Application.Tests
{
    public class MockEventRepository : IEventRepository
    {
        private List<EvenT> _events;

        public MockEventRepository()
        {
            _events = new List<EvenT>()
            {
                new EvenT() { Id = 1,
                Title= "Test1",
                Description= "Test1",
                StartDate= DateTime.Now,
                EndDate= DateTime.Now },
                new EvenT()
                {
                    Id = 2,
                Title= "Test2",
                Description= "Test2",
                StartDate= DateTime.Now,
                EndDate= DateTime.Now
                }
            };
        }

        public Task ArchiveAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            return Task.FromResult(3);
        }

        public Task<List<EvenT>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<EvenT> GetEvent(CancellationToken cancellationToken, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<EvenT> GetEventForMvc(CancellationToken cancellationToken, EvenT eventId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EvenT>> GetPendingEventsAnync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            throw new NotImplementedException();
        }
    }
}