using BankDB.Models;
using BankDB.DTOs;
using BankDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAccessController : ControllerBase
{
    private readonly IUserAccessService _userAccessService;

    public UserAccessController(IUserAccessService userAccessService)
    {
        _userAccessService = userAccessService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserAccess>>> GetUserAccesses()
    {
        var userAccesses = await _userAccessService.GetAllUserAccessAsync();
        return Ok(userAccesses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserAccess>> GetUserAccess(int id)
    {
        var userAccess = await _userAccessService.GetUserAccessByIdAsync(id);
        if (userAccess == null)
        {
            return NotFound();
        }
        return Ok(userAccess);
    }

    [HttpPost]
    public async Task<ActionResult<UserAccess>> CreateUserAccess([FromBody] UserAccess userAccess)
    {
        await _userAccessService.AddUserAccessAsync(userAccess);
        return CreatedAtAction(nameof(GetUserAccess), new { id = userAccess.UserAccessId }, userAccess);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAccess(int id, [FromBody] UserAccess userAccess)
    {
        if (id != userAccess.UserAccessId)
        {
            return BadRequest();
        }
        await _userAccessService.UpdateUserAccessAsync(userAccess);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAccess(int id)
    {
        await _userAccessService.DeleteUserAccessAsync(id);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserAccess>> GetByNumberAccessAndPassword([FromBody] LoginRequestDto loginRequest)
    {
        var userAccess = await _userAccessService.GetByNumberAccessAndPasswordAsync(loginRequest);
        if (userAccess == null)
        {
            return Unauthorized("Número de acceso o contraseña incorrectos");
        }
        return Ok(userAccess);
    }
}
