using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Application.Tests.Events;
using Events.Application.Eventz;
using static Events.Application.Tests.Events.EventsFixture;

namespace Events.Application.Tests.Events
{
    public class EventServiceTestWithDbFixture: IClassFixture<DatabaseFixture>
    {
        public DatabaseFixture Fixture;
        private EventRequestModel _requestModel;

        public EventServiceTestWithDbFixture(DatabaseFixture Fixture)
        {
            this.Fixture = Fixture;
           // _requestModel = GetRequestModel();
        }


    }
}
