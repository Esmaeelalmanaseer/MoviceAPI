
using Microsoft.EntityFrameworkCore;

namespace MoviceAPI.service
{
    public class GenraService : IGanraService
    {
        private readonly ApplictionDBContext _dbContext;

        public GenraService(ApplictionDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Genre> Add(Genre genre)
        {
           await _dbContext.AddAsync(genre);
            _dbContext.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
           return await _dbContext.genres.OrderBy(g=>g.Name).ToListAsync();
            
        }

        public async Task<Genre> GetById(int id)
        {
          var ganra=await _dbContext.genres.FindAsync(id);
            return ganra;
        }

        public async Task<bool> IsvaildGanre(int id)
        {
            return await _dbContext.genres.AnyAsync(x=>x.Id==id);
        }

        public Genre Remove(Genre genre)
        {
            _dbContext.Remove(genre);
            _dbContext.SaveChanges();
            return genre;
        }

        public Genre Update(Genre genre)
        {
            _dbContext.Update(genre);
            _dbContext.SaveChanges();
            return genre;
        }
    }
}
