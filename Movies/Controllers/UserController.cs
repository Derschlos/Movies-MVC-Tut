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
            //TODO: Bind Inputs to TranferModel  to prevent overposting

            var user = _unitOfWork.User.GetUserById(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            var userRolesInDB = await _signInManager.UserManager.GetRolesAsync(user);

            user.Email = data.User.Email;
            user.UserName = data.User.UserName;

            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();

            //2 DB calls in for loop has a lot of impact on DB performance
            foreach (var role in data.Roles)
            {
                var assingnedInDB = userRolesInDB.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assingnedInDB == null)
                    {
                        //add role
                        //await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assingnedInDB != null)
                    {
                        //remove role
                        //await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
                        rolesToRemove.Add(role.Text);
                    }
                }
            }
            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            if (rolesToRemove.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", new {Id = user.Id});
        }
    }
}
