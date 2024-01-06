namespace MoviceAPI.service
{
    public interface IGanraService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre>Add(Genre genre);
        Task<Genre> GetById(int id);
        Genre Remove(Genre genre);
        Genre Update(Genre genre);
        Task<bool> IsvaildGanre(int id);
    }
}
