using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Events.Application;
using Events.Application.Tickets;
using Events.Domain;
using Microsoft.Extensions.Logging;
using static Events.Domain.Ticket;

namespace Events.Infrastructure
{
    public class TicketRepository : ITicketRepository
    {
        private IBaseRepository<Ticket> _repository;
        private IBaseRepository<EvenT> _repositoryEvent;


        public TicketRepository(IBaseRepository<Ticket> repository, IBaseRepository<EvenT> r)
        {
            _repository = repository;
            _repositoryEvent = r;
        }

        public async Task<string> CreateTicket(CancellationToken cancellationToken, string userId, EvenT myEvent)
        {
            var guid = Guid.NewGuid().ToString();
            if (myEvent.NumOfTicket > 0)
            {
               
                var myTicket = new Ticket
                {
                    // Id = guid,
                    Status = EnumStatus.reserved,
                    EvenTId = myEvent.Id,
                    //Event = myEvent,
                    UserId = userId,
                    EventName = myEvent.Title,
                    CreatedAt = DateTime.Now

                };

                await _repository.AddAsync(cancellationToken, myTicket);
                // await _repository.SaveChanges(cancellationToken);
                myEvent.NumOfTicket--;

                await _repositoryEvent.UpdateAsync(cancellationToken, myEvent);
                //  await _repositoryEvent.SaveChanges(cancellationToken);
            }

            return guid;
            
        }
        public async Task<List<Ticket>> GetAllBoughtAsync(CancellationToken cancellationToken, string userId)
        {
            var allTickets = await _repository.GetAllAsync(cancellationToken);
            var myTicket = allTickets.Where(t => t.UserId == userId).ToList();
            var reservedticekts = myTicket.Where(t => t.Status == EnumStatus.bought).ToList();
            return reservedticekts;
        }
        public async Task BuyTicket (CancellationToken cancellationToken, int ticketId)
        {
            var AllTickets = await _repository.GetAllAsync(cancellationToken);
            var myTicket =  AllTickets.FirstOrDefault(x => x.Id == ticketId);
           myTicket.Status = EnumStatus.bought;
           await  _repository.UpdateAsync(cancellationToken, myTicket);
        }

        public async Task<List<Ticket>> GetAllAsync (CancellationToken cancellationToken, string userId)
        {
            var allTickets=  await _repository.GetAllAsync(cancellationToken);
            var myTicket =  allTickets.Where(t => t.UserId == userId).ToList();

            return myTicket;
        }

        public async Task  Delete(CancellationToken cancellationToken, Ticket ticket)
        {
          await   _repository.RemoveAsync(cancellationToken, ticket);
        }

        public async Task DeleteAll(CancellationToken cancellationToken, List <Ticket> ticket)
        {
            await _repository.RemoveAllAsync(cancellationToken, ticket);
        }

        public async Task<List<Ticket>> GetAllAsyncForWorker(CancellationToken cancellationToken)
        {

            var allTickets = await _repository.GetAllAsync(cancellationToken);

            var dueTicket = allTickets.Where(x => (DateTime.Now - x.CreatedAt).TotalMinutes > 10).ToList();

                return dueTicket;
        }
     

    }
}
