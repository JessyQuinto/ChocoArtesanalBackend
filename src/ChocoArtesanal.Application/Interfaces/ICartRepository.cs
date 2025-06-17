using ChocoArtesanal.Domain.Entities;
using System.Threading.Tasks;

namespace ChocoArtesanal.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(string userId);
        Task<Cart> SaveCartAsync(Cart cart);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(string userId);
    }
}