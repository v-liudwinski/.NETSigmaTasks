using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework17_LiudvynskyiV.S.Controllers;

[Route("[controller]")]
[ApiController]
public class ShowtimeController : Controller
{
    private readonly IShowtimeRepository _showtimeRepository;

    public ShowtimeController(IShowtimeRepository showtimeRepository)
    {
        _showtimeRepository = showtimeRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllShowtimesAsync()
    {
        var showtimes = await _showtimeRepository.GetAll();
        return Ok(showtimes);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetShowtimeByIdAsync(Guid id)
    {
        var showtime = await _showtimeRepository.Get(id);
        if (showtime is null) return NotFound();
        return Ok(showtime);
    }

    [HttpPost]
    public async Task<IActionResult> AddShowtimeAsync(ShowtimeViewModel showtimeViewModel)
    {
        var showtime = await _showtimeRepository.Add(showtimeViewModel);
        if (showtime is null) return NotFound();
        return Ok(showtime);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateShowtimeAsync(Guid id, ShowtimeViewModel showtimeViewModel)
    {
        var showtime = await _showtimeRepository.Update(id, showtimeViewModel);
        if (showtime is null) return NotFound();
        return Ok(showtime);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteShowtimeAsync(Guid id)
    {
        var showtime = await _showtimeRepository.Delete(id);
        if (showtime is null) return BadRequest();
        return Ok(showtime);
    }
}