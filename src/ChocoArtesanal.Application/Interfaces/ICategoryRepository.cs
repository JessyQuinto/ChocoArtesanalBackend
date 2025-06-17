using ChocoArtesanal.Domain.Entities;

namespace ChocoArtesanal.Application.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetBySlugAsync(string slug);
    Task<Category> CreateAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}