
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MoviceAPI.Models;

namespace MoviceAPI.service
{
    public class MovieService : ImovieService
    {
         private readonly ApplictionDBContext _dbContext;

        public MovieService(ApplictionDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> Add(Movie movie)
        {
             await _dbContext.AddAsync(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public async Task<Movie> FindById(int id)
        {
            var movice=await _dbContext.Movies.FindAsync(id);
            return movice;
        }

        public async Task<IEnumerable<Movie>> GetAll(int GenreId = 0)
        {
            return  await _dbContext.Movies.OrderByDescending(x => x.Rate)
                .Where(m=>m.Id==GenreId || GenreId==0)
                .OrderByDescending(x=>x.Rate)
                .Include(m => m.genre)
                .ToListAsync();
            
        }

        public Movie Remove(Movie movie)
        {
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public Movie Update(Movie movie)
        {
            _dbContext.Update(movie);
            _dbContext.SaveChanges();
            return movie;
        }
    }
}
