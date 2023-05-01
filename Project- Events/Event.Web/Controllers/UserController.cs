using Event.Web.Areas.Identity.Data;
using Events.Application;
using Events.Application.Userz;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<BaseUser> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<BaseUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }

        public async Task<IActionResult> Edit(CancellationToken cancellationToken, string id)
        {
            var user = await _unitOfWork.User.GetWithIdAsync(cancellationToken, id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _userManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = (IList<System.Web.Mvc.SelectListItem>)roleItems
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken, EditUserViewModel data)
        {
            var user = await _unitOfWork.User.GetWithIdAsync(cancellationToken, data.User.Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _userManager.GetRolesAsync(user);

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.UserName = data.User.UserName;
            user.PasswordHash = data.User.PasswordHash;
            // user.Email = data.User.Email;

            _unitOfWork.User.UpdateUser(cancellationToken, user);

            return RedirectToAction("Edit", new { id = user.Id });
        }
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await _userService.GetAllUsers(cancellationToken);
            return View(result);
        }
    }
}