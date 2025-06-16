using ChocoArtesanal.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChocoArtesanal.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id); // <--- AÑADE ESTE MÉTODO
        Task AddAsync(Order order);     // <--- AÑADE ESTE MÉTODO
    }
}