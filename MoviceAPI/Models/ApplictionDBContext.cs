using Microsoft.EntityFrameworkCore;

namespace MoviceAPI.Models
{
    public class ApplictionDBContext:DbContext
    {
        public ApplictionDBContext(DbContextOptions<ApplictionDBContext>options):base(options)
        {
                
        }
       public DbSet<Genre> genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
