using ChocoArtesanal.Domain.Entities;

namespace ChocoArtesanal.Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetAllAsync(int? categoryId, string? search);
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetBySlugAsync(string slug);
    Task<IEnumerable<Product>> GetFeaturedAsync();
    Task<Product> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);

    // Método agregado para corregir el error
    Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids);
}