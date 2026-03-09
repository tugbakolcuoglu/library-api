using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var loginResponse = await authService.LoginAsync(loginDto);

        if (loginResponse) return Ok();

        return Unauthorized();
    }
}