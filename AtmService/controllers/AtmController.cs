using AtmService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtmService.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService)
    {
        _atmService = atmService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _atmService.GetAtmStatus();
        return Ok(result);
    }

}