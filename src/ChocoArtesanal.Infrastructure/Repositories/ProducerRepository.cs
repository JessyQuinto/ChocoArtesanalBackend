using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using ChocoArtesanal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChocoArtesanal.Infrastructure.Repositories;

public class ProducerRepository : IProducerRepository
{
    private readonly ApplicationDbContext _context;

    public ProducerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producer>> GetAllAsync()
    {
        return await _context.Producers.ToListAsync();
    }

    public async Task<Producer?> GetByIdAsync(int id)
    {
        return await _context.Producers.FindAsync(id);
    }

    public async Task AddAsync(Producer producer)
    {
        await _context.Producers.AddAsync(producer);
    }

    public void Update(Producer producer)
    {
        _context.Producers.Update(producer);
    }

    public void Delete(Producer producer)
    {
        _context.Producers.Remove(producer);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}