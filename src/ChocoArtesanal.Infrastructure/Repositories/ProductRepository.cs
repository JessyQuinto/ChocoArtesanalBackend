using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using ChocoArtesanal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChocoArtesanal.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.Producer)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int? categoryId, string? search)
    {
        var query = context.Products
            .Include(p => p.Category)
            .Include(p => p.Producer)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.Contains(search) || 
                                   p.Description.Contains(search));
        }

        return await query.ToListAsync();
    }    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.Producer)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.Producer)
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.Producer)
            .FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<IEnumerable<Product>> GetFeaturedAsync()
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.Producer)
            .Where(p => p.Featured)
            .ToListAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}