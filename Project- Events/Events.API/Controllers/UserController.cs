using Events.API.Infrastructure.Auth;
using Events.Application.Userz;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Events.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<JWTConfiguration> _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IOptions<JWTConfiguration> options, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _options = options;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<UserResponseModel> Register(CancellationToken cancellation, UserCreateModel user)
        {
            return await _userService.CreateAsync(cancellation, user);
        }

        [Route("LogIn")]
        [HttpPost]
        public async Task<string> LogIn(CancellationToken cancellation, UserLoginRequest request)
        {
            var Id = await _userService.GetIdAsync(cancellation, request.UserName);
            var result = await _userService.AuthenticateAsync(cancellation, request.UserName, request.PasswordHash);
            var tokeen = JWTHelper.GenerateSecurityToken(result, _options, Id);
            return tokeen.ToString();
        }

        [HttpPatch]
        public async Task Patch(string username, JsonPatchDocument<UserPatchModel> updateStatus, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst("UserId")!.Value;

            var changee = await _userService.GetAsyncForPatch(cancellationToken, username, userId);
            updateStatus.ApplyTo(changee);
            await _userService.UpdateAsync(cancellationToken, changee, username);
        }

        //public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        //{
        //    var result = await _userService.GetAllUsers(cancellationToken);
        //    return View(result);
        //}
    }
}