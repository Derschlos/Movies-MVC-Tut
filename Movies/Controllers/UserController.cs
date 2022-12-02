using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Areas.Identity.Data;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<MoviesUser> _signInManager;

        public UserController(IUnitOfWork UnitOfWork, SignInManager<MoviesUser> signInManager)
        {
            _unitOfWork = UnitOfWork;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetMoviesUsers();
            return View(users);
        }
        public async Task<IActionResult> Edit(string id)
        {

            var user =_unitOfWork.User.GetUserById(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = roleItems
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUserById(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.Email = data.User.Email;
            user.UserName = data.User.UserName;
            
            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", user);
        }
    }
}
