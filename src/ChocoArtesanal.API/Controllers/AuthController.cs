using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChocoArtesanal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, IConfiguration config, IMapper mapper)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            // 1. Verificar si el correo electrónico ya está en uso
            if (await _userRepository.GetByEmailAsync(request.Email) != null)
            {
                return BadRequest("El correo electrónico ya está registrado.");
            }

            // 2. Hashear la contraseña
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 3. Crear la nueva entidad de Usuario
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                Address = request.Address,
                Phone = request.Phone,
                Role = "Cliente" // Rol por defecto
            };

            // 4. Guardar el usuario en la base de datos
            await _userRepository.AddAsync(user);

            return Ok(new { message = "Usuario registrado exitosamente." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            // 1. Buscar al usuario por su correo electrónico
            var user = await _userRepository.GetByEmailAsync(request.Email);

            // 2. Verificar si el usuario existe y si la contraseña es correcta
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // 3. Generar el Token JWT
            var token = GenerateJwtToken(user);

            // 4. Mapear el usuario a un DTO para la respuesta
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(new AuthResponseDto(token, userDto));
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims (información que viaja en el token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3), // Duración del token
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}