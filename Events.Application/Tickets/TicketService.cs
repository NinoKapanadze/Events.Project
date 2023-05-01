using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Events.Domain;
using Events.Application.Eventz;
using Mapster;
using static Events.Domain.Ticket;
using System.Reflection.Metadata.Ecma335;

namespace Events.Application.Tickets
{
    public class TicketService : ITicketService
    {
        readonly ITicketRepository _repo;
        readonly IEventRepository _repository;


        public TicketService(ITicketRepository repo, IEventRepository repository)
        {
            _repo = repo;
            _repository = repository;
        }

        public async Task<string> CreateAsync(CancellationToken cancellationToken, string eventName, string userId)
        {
            var eventList = await _repository.GetAllAsync(cancellationToken);
            var myEvent = eventList.FirstOrDefault(e => e.Title == eventName);
            var id = await _repo.CreateTicket(cancellationToken, userId, myEvent);
            return id;
        }

        public async Task BuyAsync(CancellationToken cancellationToken, int ticketId)
        {
            await _repo.BuyTicket(cancellationToken, ticketId);
        }

        public async Task<List<ViewTicketModel>> SeeTickets(CancellationToken cancellationToken, string userId)
        {
            var ticketList = await _repo.GetAllAsync(cancellationToken, userId);
            return ticketList.Adapt<List<ViewTicketModel>>();
        }
        public async Task<List<ViewTicketModel>> GetAllBoughtAsync(CancellationToken cancellationToken, string userId)
        {
            var ticketList = await _repo.GetAllBoughtAsync(cancellationToken, userId);
            return ticketList.Adapt<List<ViewTicketModel>>();

        }
        public async Task<List<ViewTicketModel>> BuyAsyncForMvc(CancellationToken cancellationToken, string userId)
        {
            var alltickets = await _repo.GetAllAsync(cancellationToken, userId);
            var reservedticekts = alltickets.Where(x => x.Status == EnumStatus.reserved).ToList();
            var a = reservedticekts.Adapt<List<ViewTicketModel>>();
            return a;
        }

        public async Task<List<EvenT>> RemoveDueTickets(CancellationToken cancellationToken)
        {
            var dueTickets = await _repo.GetAllAsyncForWorker(cancellationToken);
            var myeventlist = new List<EvenT>();

            var eventstoplus = dueTickets.Select(x => x.Event).ToList();
            await _repo.DeleteAll(cancellationToken, dueTickets);

            return eventstoplus;
        }
    }
}
