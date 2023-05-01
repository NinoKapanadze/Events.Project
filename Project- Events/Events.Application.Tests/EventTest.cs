using Events.Application.Eventz;
using Events.Domain;

namespace Events.Application.Tests
{
    public class EventTest
    {
        private readonly EvenT _myEvent;
        private readonly EventRequestModel eventRequestModel;

        public EventTest()

        {
            eventRequestModel = GetEventRequestModel();
            _myEvent = GetMyEvent();
        }

        public async void GetEvent_WhenDataIsCorrect_ShouldReturnNewEvent()
        {
            //arrange
            var eventrepository = new MockEventRepository();
            var userrepository = new MockUserRepository();
            var eventService = new EventService(eventrepository, userrepository);
            //act
            // var a = await GetEvent
            //
        }

        [Fact]
        public async void CreateAsync_WhenDataIsCorrect_ShouldReturnTitle()
        {
            //arrange
            var eventrepository = new MockEventRepository();
            var userrepository = new MockUserRepository();
            var eventService = new EventService(eventrepository, userrepository);
            var userID = "10";
            //act
            var a = await eventService.CreateAsyncWithTitle(new CancellationToken(), eventRequestModel, userID);
            //assert
            Assert.Equal("Test", a);
        }

        private EvenT GetMyEvent()
        {
            return new EvenT
            {
                Id = 3,
                Title = "Test",
                Description = "Test",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
        }

        private EventRequestModel GetEventRequestModel()
        {
            return new EventRequestModel()
            {
                Title = "Test",
                Description = "Test",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
        }
    }
}