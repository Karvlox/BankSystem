using BankDB.Models;
using BankDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _usuarioService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _usuarioService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            await _usuarioService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _usuarioService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _usuarioService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("process-license/{id}")]
        public async Task<IActionResult> ProcessLicense(int id)
        {
            var user = await _usuarioService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"No user found with ID {id}");
            }

            
            string licensePath = $"/path/to/licenses/{id}_license.jpg";
            string faceBase64 = "base64representationoftheface";

            await _usuarioService.UpdateUserLicenseAndFaceAsync(id, licensePath, faceBase64);

            return Ok(new
            {
                Message = "License processed and user updated",
                LicensePath = licensePath,
                FaceBase64 = faceBase64
            });
        }

    }
}
