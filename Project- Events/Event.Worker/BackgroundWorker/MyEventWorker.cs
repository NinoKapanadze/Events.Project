using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Events.Application.Eventz;
using Events.Application.Tickets;
using Events.Domain;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;

namespace Event.Worker.BackgroundWorker
{
    public class MyEventWorker :BackgroundService
    {
        private readonly ILogger<MyEventWorker> _logger;
        private readonly IServiceProvider _serviceProviders;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private CancellationToken cancellationToken;
        private string Schedule => "*/5 * * * * *";
        //private string Schedule12Hours => "0 */12 * * *";

        public MyEventWorker(ILogger<MyEventWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _serviceProviders = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                    await UpdateEvents(cancellationToken);
                   
                    }
                
                }
            while (!stoppingToken.IsCancellationRequested);
        }
        private void Process()
        {
            _logger.LogInformation("hello world" + DateTime.Now.ToString("F"));
        }

        public async Task UpdateEvents(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProviders.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
                var ticketrepo = scope.ServiceProvider.GetRequiredService<ITicketRepository>();
                #region ticketworker
                var allTickets = await ticketrepo.GetAllAsyncForWorker(cancellationToken);

                foreach (var ticket in allTickets)
                {
                    int i = 0;
                    if ((DateTime.Now - ticket.CreatedAt).TotalMinutes > 10)
                    {
                        var myEvent = await eventService.GetEvent(cancellationToken, ticket.EvenTId);
                        myEvent.NumOfTicket++;
                        await eventService.UpdateAsync(cancellationToken, myEvent);
                        await ticketrepo.Delete(cancellationToken, ticket);
                    }
                    else
                        continue;

                }

                #endregion
                #region eventWorker 
                var allevents = await eventService.GetAllAsyncDeepLook(cancellationToken);
                var myevents = allevents.Adapt<List<EvenT>>();
                foreach (var myEvent in myevents)
                {
                    if (DateTime.Compare(myEvent.StartDate, DateTime.Now) < 0)
                        continue;
                    else
                    {
                        await eventService.ArchiveAsync(cancellationToken, myEvent);

                    }
                }


                #endregion
            }

            }
        }
}
