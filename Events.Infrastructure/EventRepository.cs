using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Events.Application;
using Events.Application.Eventz;
using Events.Domain;
using Microsoft.Extensions.Logging;

namespace Events.Infrastructure
{
    public class EventRepository : IEventRepository
    {
        private IBaseRepository<EvenT> _repository;
        public EventRepository(IBaseRepository<EvenT> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            await _repository.AddAsync(cancellationToken, eventt);

        }

        public async Task<List<EvenT>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
        public async Task ArchiveAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            eventt.IsArchived = true;
            await _repository.UpdateAsync(cancellationToken, eventt);

        }
        public async Task<EvenT> GetEvent(CancellationToken cancellationToken, int eventId)
        {
            var allEvents = await _repository.GetAllAsync(cancellationToken);
            var myEvent =  allEvents.SingleOrDefault(x => x.Id == eventId);
            return myEvent;
        }
        public async Task<EvenT> GetEventForMvc(CancellationToken cancellationToken, EvenT eventId)
        {
            var myEvent = await _repository.GetAsync(cancellationToken, eventId);
            return myEvent;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            await _repository.UpdateAsync(cancellationToken, eventt);
        }
        public async Task<List<EvenT>> GetPendingEventsAnync(CancellationToken cancellationToken)
            {
            var allEvents = await _repository.GetAllAsync(cancellationToken);
            var myEvents = allEvents.Where(t => t.AdministratorApproved == false).ToList();
            return myEvents;
        }
        public async Task ApproveEvent(CancellationToken cancellationToken, int eventId)
        {
            var allEvents = await _repository.GetAllAsync(cancellationToken);
            var myEvent = allEvents.SingleOrDefault(x => x.Id == eventId);

            if (myEvent== null)
            {
                throw new Exception("this event does not exist");
            }
            if (myEvent.AdministratorApproved == true)
                throw new Exception("this event is apready approved");
            if (myEvent.IsArchived == true)
                throw new Exception("this event is apready archived");
            myEvent.AdministratorApproved = true;

            _repository.UpdateAsync(cancellationToken, myEvent);

        }
        public async Task ApproveEventForMvc(CancellationToken cancellationToken, EvenT eventId)
        {
            var myEvent = await _repository.GetAsync(cancellationToken, eventId);
      
            myEvent.AdministratorApproved = true;

            _repository.UpdateAsync(cancellationToken, myEvent);

        }
    }
}
