using MovieWatchlistApi.Models;

namespace MovieWatchlistApi.Services;

public interface IMovieService
{
    Task<List<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(string id);
    Task<Movie> CreateAsync(Movie movie);
    Task UpdateAsync(string id, Movie movie);
    Task DeleteAsync(string id);
}
