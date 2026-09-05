using MovieWatchlistApi.Models;

namespace MovieWatchlistApi.Repositories;

public interface IMovieRepository
{
    Task<List<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(string id);
    Task CreateAsync(Movie movie);
    Task UpdateAsync(string id, Movie movie);
    Task DeleteAsync(string id);
}
