using Events.Application.Eventz;
using Events.Domain;

namespace Events.Application.Tests.Events
{
    public class EventsTests
    {
        private EventRequestModel _requestModel;
        public EventsTests()
        {
            _requestModel = GetRequestModel();
        }

        public EventRequestModel GetRequestModel()
        {
            return new EventRequestModel
            {
                Title = "Title",
                Description = "Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                NumOfTicket = 1
            };
        }
        //[Fact]
        //public async Task <EvenT> GetEvent_WehnIsCorrect_ShouldReturnEvent(CancellationToken cancellationToken, int eventId)
        //{

        //}
    }
        
}