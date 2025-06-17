using ChocoArtesanal.Domain.Entities;

namespace ChocoArtesanal.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    // Métodos agregados para corregir errores
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User user);
    Task UpdateAsync(User user);
}