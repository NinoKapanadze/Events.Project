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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Event.Worker
{

}
//{
//    public class ApiClient
//    {
//        private readonly ILogger<ApiClient> _logger;
//        private readonly IServiceProvider _serviceProviders;
//        public ApiClient(ILogger<ApiClient> logger, IServiceProvider serviceProviders)
//        {
//            _logger = logger;
//            _serviceProviders = serviceProviders;
//        }

//        public async Task  SendEvents(CancellationToken cancellationToken )
//        {
//            using (var scope = _serviceProviders.CreateScope())
//            {
//                var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
//                var ticketrepo = scope.ServiceProvider.GetRequiredService<ITicketRepository>();
//                #region ticketworker
//                var allTickets = await ticketrepo.GetAllAsyncForWorker(cancellationToken);

//                foreach (var ticket in allTickets)
//                {
//                    int i = 0;
//                    if ((DateTime.Now - ticket.CreatedAt).TotalMinutes > 10)
//                    {
//                        var myEvent = await eventService.GetEvent(cancellationToken, ticket.EvenTId);
//                        myEvent.NumOfTick++;
//                        await eventService.UpdateAsync(cancellationToken, myEvent);
//                        await ticketrepo.Delete(cancellationToken, ticket);
//                    }
//                    else
//                        continue;

//                }

//                #endregion
//                #region eventWorker 
//               var allevents= await eventService.GetAllAsyncDeepLook(cancellationToken);
//                var myevents = allevents.Adapt<List<EvenT>>();
//                foreach (var myEvent in myevents)
//                {
//                    if (DateTime.Compare(myEvent.StartDate, DateTime.Now) < 0)
//                        continue;
//                    else
//                    {
//                       await eventService.ArchiveAsync(cancellationToken, myEvent);

//                    }
//                }
                

//                #endregion




//                var json = JsonConvert.SerializeObject(EventtList);

//            var data = new StringContent(json, Encoding.UTF8, "application/json");

//            var url = "https://localhost:7226/api/Rate";

//            var httpClientHandler = new HttpClientHandler();

//            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
//            {
//                return true;
//            };

//            using var httpClient = new HttpClient(httpClientHandler);
//            try
//            {
//                var response = await httpClient.PutAsync(url, data);

//                string result = await response.Content.ReadAsStringAsync();

//                _logger.LogInformation(result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.ToString());
//            }
//        }
//    }
//}
