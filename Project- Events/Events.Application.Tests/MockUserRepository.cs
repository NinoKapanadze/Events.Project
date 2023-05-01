using Event.Web.Areas.Identity.Data;
using Events.Application.Userz;

namespace Events.Application.Tests
{
    public class MockUserRepository : IUserRepository
    {
        public Task<BaseUser> CreateAsync(CancellationToken cancellationToken, BaseUser person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(CancellationToken cancellationToken, string username)
        {
            throw new NotImplementedException();
        }

        public Task<BaseUser> GetAsync(CancellationToken cancellationToken, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetByIdAsync(CancellationToken cancellationToken, string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<BaseUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<BaseUser> GetWithIdAsync(CancellationToken cancellationToken, string id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseUser> GetWithUsernameAsync(CancellationToken cancellationToken, string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CancellationToken cancellationToken, BaseUser baseuser)
        {
            throw new NotImplementedException();
        }

        public Task<BaseUser> UpdateUser(CancellationToken cancellationToken, BaseUser user)
        {
            throw new NotImplementedException();
        }
    }
}