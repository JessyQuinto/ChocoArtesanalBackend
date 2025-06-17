using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChocoArtesanal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protege todo el controlador
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IOrderRepository orderRepository, IMapper mapper)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var orderDto = await _orderService.CreateOrderAsync(request, userId);
                return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.Id }, orderDto);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderDto>(order));
        }        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var orders = await _orderRepository.GetByUserIdAsync(int.Parse(userId));
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }
    }
}