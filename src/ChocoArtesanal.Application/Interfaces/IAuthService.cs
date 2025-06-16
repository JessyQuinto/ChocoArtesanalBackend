using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Domain.Entities;
using System.Threading.Tasks;

namespace ChocoArtesanal.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequestDto request);
        Task<string> LoginAsync(LoginRequestDto request);
    }
}