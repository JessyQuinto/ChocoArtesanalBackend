using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChocoArtesanal.Application.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper) : IAuthService
{
    public async Task<LoginResponseDto?> Login(LoginRequestDto request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var userDto = mapper.Map<UserDto>(user);

        return new LoginResponseDto(tokenString, userDto);
    }

    public async Task<bool> Register(RegisterRequestDto request)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return false;
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Address = request.Address,
            Phone = request.Phone,
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.CreateAsync(user);
        return true;
    }
}