namespace ChocoArtesanal.Application.Dtos;

// DTO para el request de registro (ya lo tenías)
public record RegisterRequestDto(string Name, string Email, string Password, string? Address, string? Phone);

// DTO para el request de inicio de sesión
public record LoginRequestDto(string Email, string Password);

// DTO para devolver los datos del usuario de forma segura
public record UserDto(int Id, string Name, string Email, string? Address, string? Phone, string Role);

// DTO para la respuesta de autenticación exitosa
public record AuthResponseDto(string Token, UserDto User);