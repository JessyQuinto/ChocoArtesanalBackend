using ChocoArtesanal.Domain.Entities;

namespace ChocoArtesanal.Application.Interfaces;

public interface IProducerRepository
{
    Task<IEnumerable<Producer>> GetAllAsync();
    Task<Producer?> GetByIdAsync(int id);
    Task AddAsync(Producer producer);
    void Update(Producer producer);
    void Delete(Producer producer);
    Task<bool> SaveChangesAsync();
}