using Homework15_LiudvynskyiV.S.Models.ViewModels;
using Homework15_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework15_LiudvynskyiV.S.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAll();
        return Ok(users);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.Get(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync(UserViewModel userViewModel)
    {
        var user = await _userRepository.Add(userViewModel);
        if (user is null) return NotFound();
        return Ok(user);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync(Guid id, UserViewModel userViewModel)
    {
        var user = await _userRepository.Update(id, userViewModel);
        if (user is null) return NotFound();
        return Ok(user);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.Delete(id);
        if (user is null) return BadRequest();
        return Ok(user);
    }
}