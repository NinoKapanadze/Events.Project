using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace MyEvent.Worker.BackgroundWorker
{
    public class MyEventWorker : BackgroundService
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
                //_schedule.GetNextOccurrence(now);
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
                var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();



                #region ticketworker


                var myEvents = await ticketService.RemoveDueTickets(cancellationToken);
                foreach (var e in myEvents)
                {
                    if (e != null)
                    {
                        e.NumOfTicket++;
                        await eventService.UpdateAsync(cancellationToken, e);
                    }
                }
                #endregion

                #region eventWorker 
                var allevents = await eventService.GetAllAsyncDeepLook(cancellationToken);
                var myevents = allevents.Adapt<List<EvenT>>();
                foreach (var myEvent in myevents)
                {
                    if ((myEvent.StartDate - DateTime.Now ).TotalDays > 5)
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
