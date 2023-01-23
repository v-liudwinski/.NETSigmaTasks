using Homework15_LiudvynskyiV.S.Models.ViewModels;
using Homework15_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework15_LiudvynskyiV.S.Controllers;

[Route("[controller]")]
[ApiController]
public class HallController : Controller
{
    private readonly IHallRepository _hallRepository;

    public HallController(IHallRepository hallRepository)
    {
        _hallRepository = hallRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHallsAsync()
    {
        var halls = await _hallRepository.GetAll();
        return Ok(halls);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetHallByIdAsync(Guid id)
    {
        var hall = await _hallRepository.Get(id);
        if (hall is null) return NotFound();
        return Ok(hall);
    }

    [HttpPost]
    public async Task<IActionResult> AddHallAsync(HallViewModel hallViewModel)
    {
        var cinema = await _hallRepository.Add(hallViewModel);
        if (cinema is null) return NotFound();
        return Ok(cinema);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateHallAsync(Guid id, HallViewModel hallViewModel)
    {
        var hall = await _hallRepository.Update(id, hallViewModel);
        if (hall is null) return NotFound();
        return Ok(hall);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteHallAsync(Guid id)
    {
        var hall = await _hallRepository.Delete(id);
        if (hall is null) return BadRequest();
        return Ok(hall);
    }
}