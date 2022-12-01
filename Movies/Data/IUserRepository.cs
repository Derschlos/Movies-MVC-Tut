using Movies.Areas.Identity.Data;

namespace Movies.Data
{
    public interface IUserRepository
    {
        ICollection<MoviesUser> GetMoviesUsers();
        MoviesUser GetUserById(string id);
    }
}
