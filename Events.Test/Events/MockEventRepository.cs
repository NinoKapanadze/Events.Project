using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Application.Eventz;
using Events.Domain;

namespace Events.Application.Tests.Events
{
    public class MockEventRepository : IEventRepository
    {
        private List<EvenT> _events;
        public MockEventRepository()
        {
            _events = new List<EvenT>()
            {
                new EvenT() {Id= 1, AdministratorApproved = true, BaseUserId = "123123123", Description= "Demo Project for TBC academy presentation about events", Title= "Demo presentation", IsArchived= false, EndDate= DateTime.Now.AddDays(5), NumOfTicket= 5, StartDate= DateTime.Now.AddDays(2) }
            };
        }
        public Task ArchiveAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            return Task.FromResult(4);

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
