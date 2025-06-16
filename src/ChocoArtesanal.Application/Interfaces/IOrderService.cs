using ChocoArtesanal.Application.Dtos;
using System.Threading.Tasks;

namespace ChocoArtesanal.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderRequestDto request, string userId);
    }
}