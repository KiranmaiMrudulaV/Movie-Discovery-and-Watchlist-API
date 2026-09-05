using MongoDB.Driver;
using MovieWatchlistApi.Models;

namespace MovieWatchlistApi.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly IMongoCollection<Movie> _movies;

    public MovieRepository(IMongoDatabase database)
    {
        _movies = database.GetCollection<Movie>("Movies");
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        return await _movies.Find(_ => true).ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(string id)
    {
        return await _movies.Find(movie => movie.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Movie movie)
    {
        await _movies.InsertOneAsync(movie);
    }

    public async Task UpdateAsync(string id, Movie movie)
    {
        await _movies.ReplaceOneAsync(existing => existing.Id == id, movie);
    }

    public async Task DeleteAsync(string id)
    {
        await _movies.DeleteOneAsync(movie => movie.Id == id);
    }
}
