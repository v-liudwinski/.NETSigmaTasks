using Homework15_LiudvynskyiV.S.Models.ViewModels;
using Homework15_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework15_LiudvynskyiV.S.Controllers;

[Route("[controller]")]
[ApiController]
public class TicketController : Controller
{
    private readonly ITicketRepository _ticketRepository;

    public TicketController(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTicketsAsync()
    {
        var tickets = await _ticketRepository.GetAll();
        return Ok(tickets);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetTicketByIdAsync(Guid id)
    {
        var ticket = await _ticketRepository.Get(id);
        if (ticket is null) return NotFound();
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> AddTicketAsync(TicketViewModel ticketViewModel)
    {
        var ticket = await _ticketRepository.Add(ticketViewModel);
        if (ticket is null) return NotFound();
        return Ok(ticket);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTicketAsync(Guid id, TicketViewModel ticketViewModel)
    {
        var ticket = await _ticketRepository.Update(id, ticketViewModel);
        if (ticket is null) return NotFound();
        return Ok(ticket);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteTicketAsync(Guid id)
    {
        var ticket = await _ticketRepository.Delete(id);
        if (ticket is null) return BadRequest();
        return Ok(ticket);
    }
}