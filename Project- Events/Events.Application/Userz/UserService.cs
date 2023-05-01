using Event.Web.Areas.Identity.Data;
using Events.Application.Exceptions;
using Mapster;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using static Event.Web.Areas.Identity.Data.BaseUser;

//using Mapster;

namespace Events.Application.Userz
{
    public class UserService : IUserService
    {
        private const string SECRET_KEY = "asldij23324";
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> AuthenticateAsync(CancellationToken cancellationToken, string username, string password)
        {
            var userEntity = await _repository.GetAsync(cancellationToken, username, GenerateHash(password));

            if (userEntity == null)
                throw new InvalidUserException("username or password is incorrect");

            return userEntity.UserName.ToString();
        }

        public async Task<string> GetIdAsync(CancellationToken cancellationToken, string username)
        {
            return await _repository.GetByIdAsync(cancellationToken, username);
        }

        public async Task<UserResponseModel> CreateAsync(CancellationToken cancellationToken, UserCreateModel userModel)
        {
            var exists = await _repository.Exists(cancellationToken, userModel.Username);

            if (exists)
                throw new UserAlreadyExistsException("user already exists");
            var username = userModel.Username;

            var hashpassword = GenerateHash(userModel.Password);
            var userEntity = userModel.Adapt<BaseUser>();
            userEntity.PasswordHash = hashpassword;
            userEntity.UserName = username;
            var existedUser = await _repository.CreateAsync(cancellationToken, userEntity);

            return existedUser.Adapt<UserResponseModel>();
        }

        private string GenerateHash(string input)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        //no erors here yet-------------------------------------------------------------------------------------
        public async Task<UserPatchModel> GetAsyncForPatch(CancellationToken cancellationToken, string username, string changerId)
        {
            var result = await _repository.GetWithUsernameAsync(cancellationToken, username);
            var changer = await _repository.GetWithIdAsync(cancellationToken, changerId);
            if (changer.Role != EnumRole.Administrator)
                throw new NotAdministratorException("Only Administrator can do given task");
            else
                return result.Adapt<UserPatchModel>();
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, UserPatchModel person, string username)
        {
            if (!await _repository.Exists(cancellationToken, username))
                throw new Exception("Not Found");
            BaseUser checkperson = await _repository.GetWithUsernameAsync(cancellationToken, username);
            var newrole = person.Role;
            checkperson.Role = newrole;
            object value = _repository.UpdateUser(cancellationToken, checkperson);
        }

        async Task<BaseUser> IUserService.GetWithIdAsync(CancellationToken cancellationToken, string id)
        {
            return await _repository.GetWithIdAsync(cancellationToken, id);
        }
        public async Task<List<UserResponseModel>> GetAllUsers(CancellationToken cancellationToken)
        {

            var result =  await _repository.GetBaseUsersAsync(cancellationToken);
                return result.Adapt<List<UserResponseModel>>();

        }

    }
}