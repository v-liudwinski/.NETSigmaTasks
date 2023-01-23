using Homework15_LiudvynskyiV.S.Models.ViewModels;
using Homework15_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework15_LiudvynskyiV.S.Controllers;

[Route("[controller]")]
[ApiController]
public class SeatController : Controller
{
    private readonly ISeatRepository _seatRepository;

    public SeatController(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllSeatsAsync()
    {
        var seats = await _seatRepository.GetAll();
        return Ok(seats);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetSeatByIdAsync(Guid id)
    {
        var seat = await _seatRepository.Get(id);
        if (seat is null) return NotFound();
        return Ok(seat);
    }

    [HttpPost]
    public async Task<IActionResult> AddSeatAsync(SeatViewModel seatViewModel)
    {
        var seat = await _seatRepository.Add(seatViewModel);
        if (seat is null) return NotFound();
        return Ok(seat);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateSeatAsync(Guid id, SeatViewModel seatViewModel)
    {
        var seat = await _seatRepository.Update(id, seatViewModel);
        if (seat is null) return NotFound();
        return Ok(seat);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteSeatAsync(Guid id)
    {
        var seat = await _seatRepository.Delete(id);
        if (seat is null) return BadRequest();
        return Ok(seat);
    }
}