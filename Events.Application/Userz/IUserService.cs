using Event.Web.Areas.Identity.Data;

namespace Events.Application.Userz
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(CancellationToken cancellation, string username, string password);

        Task<UserResponseModel> CreateAsync(CancellationToken cancellation, UserCreateModel user);

        Task<UserPatchModel> GetAsyncForPatch(CancellationToken cancellationToken, string username, string changerId);

        Task<string> GetIdAsync(CancellationToken cancellationToken, string username);

        Task UpdateAsync(CancellationToken cancellationToken, UserPatchModel person, string userId);

        Task<BaseUser> GetWithIdAsync(CancellationToken cancellationToken, string id);
        Task<List<UserResponseModel>> GetAllUsers(CancellationToken cancellationToken);
    }
}