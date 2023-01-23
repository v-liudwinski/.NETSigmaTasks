using Homework15_LiudvynskyiV.S.Models.ViewModels;
using Homework15_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework15_LiudvynskyiV.S.Controllers;

[Route("[controller]")]
[ApiController]
public class MovieController : Controller
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovieAsync()
    {
        var movies = await _movieRepository.GetAll();
        return Ok(movies);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetMovieByIdAsync(Guid id)
    {
        var movie = await _movieRepository.Get(id);
        if (movie is null) return NotFound();
        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> AddMovieAsync(MovieViewModel movieViewModel)
    {
        var movie = await _movieRepository.Add(movieViewModel);
        if (movie is null) return NotFound();
        return Ok(movie);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateMovieAsync(Guid id, MovieViewModel movieViewModel)
    {
        var movie = await _movieRepository.Update(id, movieViewModel);
        if (movie is null) return NotFound();
        return Ok(movie);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteMovieAsync(Guid id)
    {
        var movie = await _movieRepository.Delete(id);
        if (movie is null) return BadRequest();
        return Ok(movie);
    }
}