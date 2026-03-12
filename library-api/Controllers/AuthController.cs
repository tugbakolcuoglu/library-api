using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Services.Interfaces;
using WebApplication2.VMs;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestVm loginRequest)
    {

        var loginDto = new LoginRequestDto(loginRequest.Username, loginRequest.Password);
        
        var result = await authService.LoginAsync(loginDto);

        if (!result)
            return Unauthorized("Kullanıcı adı veya şifre hatalı");

        return Ok("Login başarılı");
    }
} 
