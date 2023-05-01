using Events.Application.Eventz.Models;
using Events.Application.Userz;
using Events.Domain;
using Mapster;
using System.Data;

namespace Events.Application.Eventz
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        private readonly IUserRepository _userrepo;

        public EventService(IEventRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userrepo = userRepository;
        }

        public async Task CreateAsync(CancellationToken cancellationToken, EventRequestModel request, string userId)
        {
            var myevent = request.Adapt<EvenT>();
            myevent.BaseUserId = userId;
            await _repo.CreateAsync(cancellationToken, myevent);
        }

        public async Task<string> CreateAsyncWithTitle(CancellationToken cancellationToken, EventRequestModel request, string userId)
        {
            var myevent = request.Adapt<EvenT>();
            myevent.BaseUserId = userId;
            await _repo.CreateAsync(cancellationToken, myevent);
            return myevent.Title;
        }

        public async Task CreateForMvc(CancellationToken cancellationToken, EventRequestModel request)
        {
            var myevent = request.Adapt<EvenT>();
            myevent.AdministratorApproved = false;
            await _repo.CreateAsync(cancellationToken, myevent);
        }

        public async Task<List<EventFirstLookModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync(cancellationToken);
            var a = result.Where(x => x.AdministratorApproved == true).ToList();
            var b = a.Where(x => x.IsArchived == false).ToList();
            return b.Adapt<List<EventFirstLookModel>>();
        }

        public async Task<List<EventDeepLookModel>> GetAllAsyncDeepLook(CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync(cancellationToken);
            var a = result.Where(x => x.AdministratorApproved == true).ToList();
            var b = a.Where(x => x.IsArchived == false).ToList();
            return b.Adapt<List<EventDeepLookModel>>();
        }

        public async Task<EvenT> GetEvent(CancellationToken cancellationToken, int eventId)
        {
            var myEvent = await _repo.GetEvent(cancellationToken, eventId);
            if (myEvent is null)
            {
                throw new Exception("event with this id does not exist");
            }
            if (myEvent.IsArchived = true)
            {
                throw new Exception("this event IServiceProvider apready archived");
            }
            if (myEvent.AdministratorApproved = false)
            {
                throw new Exception("this event is not  approved!");
            }
            return myEvent;
        }

        public async Task ArchiveAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            var eventToArchive = await _repo.GetEvent(cancellationToken, eventt.Id);
            await _repo.ArchiveAsync(cancellationToken, eventToArchive);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, EvenT eventt)
        {
            _repo.UpdateAsync(cancellationToken, eventt);
        }

        public async Task<List<EventDeepLookModel>> SeePendingEvents(CancellationToken cancellationToken, string userId)
        {
            var user = await _userrepo.GetWithIdAsync(cancellationToken, userId);
            //if( user.Role == EnumRole.User)
            // {
            //     throw new Exception("only administrator or manager can access this");
            // }
            var myEvents = await _repo.GetPendingEventsAnync(cancellationToken);
            var a = myEvents.Adapt<List<EventDeepLookModel>>();
            return a;
        }

        public async Task<List<EventDeepLookModel>> SeePendingEventsForMvc(CancellationToken cancellationToken)
        {
            var myEvents = await _repo.GetPendingEventsAnync(cancellationToken);
            var a = myEvents.Adapt<List<EventDeepLookModel>>();
            return a;
        }

        public async Task ApproveEvent(CancellationToken cancellationToken, int eventId)
        {
            var myEvent = await _repo.GetEvent(cancellationToken, eventId);
            if (myEvent is null)
            {
                throw new Exception("event with this id does not exist");
            }
            //if(myEvent.IsArchived=true)
            //{
            //    throw new Exception("this event IServiceProvider apready archived");
            //}
            //if (myEvent.AdministratorApproved = true)
            //{
            //    throw new Exception("this event is already approved!");
            //}
            myEvent.AdministratorApproved = true;
            await _repo.UpdateAsync(cancellationToken, myEvent);
        }

        public async Task ApproveEventForMvc(CancellationToken cancellationToken, EventDeepLookModel e)
        {
            var m = e.Adapt<EvenT>();

            m.AdministratorApproved = true;
            await _repo.UpdateAsync(cancellationToken, m);
        }
    }
}