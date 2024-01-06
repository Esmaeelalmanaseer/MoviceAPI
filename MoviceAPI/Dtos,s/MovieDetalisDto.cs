namespace MoviceAPI.Models
{
    public class MovieDetalisDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string Storeine { get; set; }
        public byte[] Poster { get; set; }
        public int GenreId { get; set; }
        public string genre { get; set; }
    }
}
