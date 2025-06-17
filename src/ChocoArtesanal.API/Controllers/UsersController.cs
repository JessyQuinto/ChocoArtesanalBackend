using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChocoArtesanal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    [HttpGet("profile")]
    public async Task<ActionResult<UserDto>> GetProfile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(mapper.Map<UserDto>(user));
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateUserDto updateUserDto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        mapper.Map(updateUserDto, user);

        await userRepository.UpdateAsync(user);

        return NoContent();
    }
}