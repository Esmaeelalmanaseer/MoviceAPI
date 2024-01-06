using Microsoft.EntityFrameworkCore.Update.Internal;

namespace MoviceAPI.service
{
    public interface ImovieService
    {
        Task<IEnumerable<Movie>> GetAll(int GenreId=0);
        Task<Movie> FindById(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Movie Remove(Movie movie);
    }
}
