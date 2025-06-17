namespace ChocoArtesanal.Application.Dtos;

public record RegisterRequestDto(string Name, string Email, string Password, string? Address, string? Phone);

public record LoginRequestDto(string Email, string Password);

public record UserDto(int Id, string Name, string Email, string? Address, string? Phone);

public record LoginResponseDto(string Token, UserDto User);

public record UpdateUserDto(string Name, string? Address, string? Phone);