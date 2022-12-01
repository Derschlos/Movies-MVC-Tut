using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Areas.Identity.Data;

namespace Movies.Models
{
    public class EditUserViewModel
    {
        public MoviesUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }
    }
}
