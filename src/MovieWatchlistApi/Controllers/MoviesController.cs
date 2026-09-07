using Microsoft.AspNetCore.Mvc;
using MovieWatchlistApi.Models;
using MovieWatchlistApi.Services;

namespace MovieWatchlistApi.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetAll()
    {
        var movies = await _movieService.GetAllAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(string id)
    {
        var movie = await _movieService.GetByIdAsync(id);

        if (movie is null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> Create(Movie movie)
    {
        var created = await _movieService.CreateAsync(movie);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Movie movie)
    {
        var existing = await _movieService.GetByIdAsync(id);

        if (existing is null)
        {
            return NotFound();
        }

        await _movieService.UpdateAsync(id, movie);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existing = await _movieService.GetByIdAsync(id);

        if (existing is null)
        {
            return NotFound();
        }

        await _movieService.DeleteAsync(id);
        return NoContent();
    }
}
