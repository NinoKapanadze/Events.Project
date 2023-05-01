using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Web.Areas.Identity.Data;
using Events.Application.Eventz;
using Microsoft.EntityFrameworkCore;

namespace Events.Application.Tests.Events
{
    public class EventsFixture : IDisposable
    {
        public EventService eventService { get;  set; }
        public EventsFixture()
        {
            var repository = new MockEventRepository();
          //  eventService = new (repository);

        }
        public void Dispose()
        {
        }

        public class DatabaseFixture : IDisposable
        {
            public UserContext Context { get; set; }

            public DatabaseFixture()
            {
                var options = new DbContextOptionsBuilder<UserContext>()
                    .UseInMemoryDatabase(databaseName: "PersonManagement")
                    .Options;

                Context = new UserContext(options);
            }

            public void Dispose()
            {
                Context.Dispose();
            }
        }
    }
}
