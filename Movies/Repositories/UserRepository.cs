using Movies.Areas.Identity.Data;
using Movies.Data;

namespace Movies.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesLoginContext _context;

        public UserRepository(MoviesLoginContext context)
        {
            _context = context;

        }
        public ICollection<MoviesUser> GetMoviesUsers()
        {
            return _context.Users.ToList();
        }

        public MoviesUser GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
