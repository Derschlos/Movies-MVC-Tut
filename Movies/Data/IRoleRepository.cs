using Microsoft.AspNetCore.Identity;
using Movies.Areas.Identity.Data;

namespace Movies.Data
{
    public interface IRoleRepository
    {//should have been IRoleRepository since Role is also a Table (where the users get assigned the roles)
        ICollection<IdentityRole> GetRoles();
    }
}
