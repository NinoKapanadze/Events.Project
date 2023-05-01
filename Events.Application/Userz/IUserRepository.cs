using Event.Web.Areas.Identity.Data;

namespace Events.Application.Userz
{
    public interface IUserRepository
    {
        Task<BaseUser> GetWithUsernameAsync(CancellationToken cancellationToken, string username);

        Task<string> GetByIdAsync(CancellationToken cancellationToken, string id);

        Task<BaseUser> GetAsync(CancellationToken cancellationToken, string username, string password);

        Task<BaseUser> GetWithIdAsync(CancellationToken cancellationToken, string id);

        Task<bool> Exists(CancellationToken cancellationToken, string username);

        //Task<bool>ExistsWithId(CancellationToken cancellationToken, int id);
        Task<BaseUser> CreateAsync(CancellationToken cancellationToken, BaseUser person); //

        ICollection<BaseUser> GetUsers();

        Task UpdateAsync(CancellationToken cancellationToken, BaseUser baseuser);

        Task<BaseUser> UpdateUser(CancellationToken cancellationToken, BaseUser user);
        Task<List<BaseUser>> GetBaseUsersAsync(CancellationToken cancellationToken);
    }
}