namespace MoviceAPI.Models
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string Storeine { get; set; }
        public IFormFile? Poster { get; set; }
        public int GenreId { get; set; }
    }
}
