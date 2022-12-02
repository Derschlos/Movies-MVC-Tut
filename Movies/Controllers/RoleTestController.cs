using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;

namespace Movies.Controllers
{
    
    public class RoleTestController : Controller
    {

        //[Authorize(Policy = "EmployeeOnly")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize(Policy = $"{Constants.Policies.RequireManager}")]
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Policy = $"{Constants.Policies.RequireAdmin}")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = $"{Constants.Policies.RequireUser}")]
        public IActionResult User()
        {
            return View();
        }
    }
}
