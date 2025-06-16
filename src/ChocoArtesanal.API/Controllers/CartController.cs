using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChocoArtesanal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere autenticación
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartRepository.GetCartAsync(userId);
            return Ok(cart ?? new Cart(userId));
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateCart(Cart cart)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cart.UserId = userId; // Asegura que el carrito pertenece al usuario autenticado
            var updatedCart = await _cartRepository.UpdateCartAsync(cart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartRepository.DeleteCartAsync(userId);
            return NoContent();
        }
    }
}