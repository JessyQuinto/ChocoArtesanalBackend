using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using ChocoArtesanal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChocoArtesanal.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    // Implementación de método agregado
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    // Implementación de método agregado
    public async Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    // Implementación de método agregado
    public async Task UpdateAsync(User user)
    {
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}