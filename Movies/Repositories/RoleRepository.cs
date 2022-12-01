using Microsoft.AspNetCore.Identity;
using Movies.Areas.Identity.Data;
using Movies.Data;

namespace Movies.Repositories
{ //should have been RoleRepository since Role is also a Table (where the users get assigned the roles)
    public class RoleRepository : IRoleRepository
    {
        private readonly MoviesLoginContext _context;
        public RoleRepository(MoviesLoginContext context)
        {
            _context = context;
        }

        

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
