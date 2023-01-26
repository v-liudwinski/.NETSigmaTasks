using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework17_LiudvynskyiV.S.Controllers;

[Route("[controller]")]
[ApiController]
public class CinemaController : Controller
{
    private readonly ICinemaRepository _cinemaRepository;

    public CinemaController(ICinemaRepository cinemaRepository)
    {
        _cinemaRepository = cinemaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCinemasAsync()
    {
        var cinemas = await _cinemaRepository.GetAll();
        return Ok(cinemas);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetCinemaByIdAsync(Guid id)
    {
        var cinema = await _cinemaRepository.Get(id);
        if (cinema is null) return NotFound();
        return Ok(cinema);
    }

    [HttpPost]
    public async Task<IActionResult> AddCinemaAsync(CinemaViewModel cinemaViewModel)
    {
        var cinema = await _cinemaRepository.Add(cinemaViewModel);
        if (cinema is null) return NotFound();
        return Ok(cinema);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCinemaAsync(Guid id, CinemaViewModel cinemaViewModel)
    {
        var cinema = await _cinemaRepository.Update(id, cinemaViewModel);
        if (cinema is null) return NotFound();
        return Ok(cinema);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCinemaAsync(Guid id)
    {
        var cinema = await _cinemaRepository.Delete(id);
        if (cinema is null) return BadRequest();
        return Ok(cinema);
    }
}