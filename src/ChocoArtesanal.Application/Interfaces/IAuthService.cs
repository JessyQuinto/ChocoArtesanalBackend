using ChocoArtesanal.Application.Dtos;

namespace ChocoArtesanal.Application.Interfaces;

public interface IAuthService
{
    Task<bool> Register(RegisterRequestDto request);
    Task<LoginResponseDto?> Login(LoginRequestDto request);
}