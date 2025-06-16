using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChocoArtesanal.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderRequestDto request, string userId)
        {
            var cart = await _cartRepository.GetCartAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                throw new ApplicationException("Cart is empty.");
            }

            var productIds = cart.Items.Select(i => i.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);

            decimal total = 0;
            var orderDetails = cart.Items.Select(item =>
            {
                var product = products.First(p => p.Id == item.ProductId);
                var itemPrice = product.DiscountedPrice ?? product.Price;
                total += itemPrice * item.Quantity;
                return new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = itemPrice
                };
            }).ToList();

            var order = new Order
            {
                UserId = int.Parse(userId),
                Total = total,
                ShippingAddress = request.ShippingAddress,
                PaymentMethod = request.PaymentMethod,
                Status = "Pending",
                PaymentStatus = "Pending",
                OrderDetails = orderDetails
            };

            await _orderRepository.AddAsync(order);
            await _cartRepository.DeleteCartAsync(userId);

            return _mapper.Map<OrderDto>(order);
        }
    }
}