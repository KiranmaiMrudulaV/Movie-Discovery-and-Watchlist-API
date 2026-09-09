using MovieWatchlistApi.Models;
using MovieWatchlistApi.Repositories;

namespace MovieWatchlistApi.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        return await _movieRepository.GetAllAsync();
    }

    public async Task<Movie?> GetByIdAsync(string id)
    {
        return await _movieRepository.GetByIdAsync(id);
    }

    public async Task<Movie> CreateAsync(Movie movie)
    {
        ValidateReleaseYear(movie.ReleaseYear);
        movie.Id = string.Empty; // always let MongoDB generate a fresh id — ignore anything the client sent
        await _movieRepository.CreateAsync(movie);
        return movie;
    }

    public async Task UpdateAsync(string id, Movie movie)
    {
        ValidateReleaseYear(movie.ReleaseYear);
        movie.Id = id; // the URL is the source of truth for identity, never the request body
        await _movieRepository.UpdateAsync(id, movie);
    }

    public async Task DeleteAsync(string id)
    {
        await _movieRepository.DeleteAsync(id);
    }

    private static void ValidateReleaseYear(int releaseYear)
    {
        if (releaseYear < 1888 || releaseYear > DateTime.UtcNow.Year + 1)
        {
            throw new ValidationException($"Release year {releaseYear} is not valid.");
        }
    }
}
