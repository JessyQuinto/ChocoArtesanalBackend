using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChocoArtesanal.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }        public async Task<Cart?> GetCartAsync(string userId)
        {
            var cart = await _redisCache.GetStringAsync(userId);
            if (string.IsNullOrEmpty(cart))
            {
                return null;
            }
            return JsonSerializer.Deserialize<Cart>(cart);
        }

        public async Task<Cart> SaveCartAsync(Cart cart)
        {
            await _redisCache.SetStringAsync(cart.UserId, JsonSerializer.Serialize(cart));
            return cart;
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            await _redisCache.SetStringAsync(cart.UserId, JsonSerializer.Serialize(cart));
            return cart;
        }

        public async Task DeleteCartAsync(string userId)
        {
            await _redisCache.RemoveAsync(userId);
        }
    }
}