using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChocoArtesanal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var success = await authService.Register(request);
        if (!success)
        {
            return BadRequest("User with this email already exists.");
        }
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
    {
        var response = await authService.Login(request);
        if (response == null)
        {
            return Unauthorized("Invalid credentials.");
        }
        return Ok(response);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // En una implementación real, aquí se invalidaría el token (ej. con una blacklist)
        return Ok("Logged out successfully.");
    }
}