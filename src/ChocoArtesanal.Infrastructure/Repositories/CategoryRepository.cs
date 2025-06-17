using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using ChocoArtesanal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChocoArtesanal.Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task<Category?> GetBySlugAsync(string slug)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
    }

    public async Task UpdateAsync(Category category)
    {
        context.Entry(category).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}