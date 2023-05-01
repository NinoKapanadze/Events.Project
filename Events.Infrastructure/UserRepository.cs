using Event.Web.Areas.Identity.Data;
using Events.Application;
using Events.Application.Userz;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        #region Private Members

        //HAS A Composition

        private IBaseRepository<BaseUser> _repository;
        private readonly UserContext _context;

        #endregion Private Members

        #region Ctor

        public UserRepository(IBaseRepository<BaseUser> repository, UserContext context)
        {
            _repository = repository;
            _context = context;
        }

        #endregion Ctor

        public async Task<BaseUser> GetAsync(CancellationToken cancellationToken, string username, string password)
        {
            return await _repository.Table.SingleOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }

        public async Task<BaseUser> GetWithUsernameAsync(CancellationToken cancellationToken, string username)
        {
            return await _repository.Table.SingleOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }

        async Task<BaseUser> IUserRepository.GetWithIdAsync(CancellationToken cancellationToken, string id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public async Task<string> GetByIdAsync(CancellationToken cancellationToken, string username)
        {
            var user = await _repository.Table.SingleOrDefaultAsync(x => x.UserName == username);
            if (user == null) { throw new Exception("this login does not exist please register first"); };
            return user.Id.ToString();
        }

        ICollection<BaseUser> IUserRepository.GetUsers()
        {
            return _context.Users.ToList();
        }

        //public async Task<bool> ExistsWithId(CancellationToken cancellationToken, int id)
        //{
        //    var ids = id.ToString();
        //    return await _repository.AnyAsync(cancellationToken, x => x.Id == ids);
        //}
        public async Task<BaseUser> CreateAsync(CancellationToken cancellationToken, BaseUser person)
        {
            await _repository.AddAsync(cancellationToken, person);
            return person;
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, string username)
        {
            return await _repository.AnyAsync(cancellationToken, x => x.UserName == username);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, BaseUser baseuser)
        {
            _context.Update(baseuser);
            _context.SaveChanges();
        }

        async Task<BaseUser> IUserRepository.UpdateUser(CancellationToken cancellationToken, BaseUser user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<List<BaseUser>> GetBaseUsersAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync( cancellationToken);    
        }
    }
}