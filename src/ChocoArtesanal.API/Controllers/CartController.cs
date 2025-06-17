using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChocoArtesanal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var cart = await _cartRepository.GetCartAsync(userId);
        return Ok(cart ?? new Cart(userId));
    }

    [HttpPost]
    public async Task<IActionResult> SaveCart([FromBody] SaveCartDto cartDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var cart = new Cart(userId);
        cart.Items = cartDto.Items.Select(item => new CartItem
        {
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Price = item.Price,
            Quantity = item.Quantity
        }).ToList();

        await _cartRepository.SaveCartAsync(cart);
        return Ok(new { Message = "Cart saved successfully." });
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _cartRepository.DeleteCartAsync(userId);
        return Ok(new { Message = "Cart cleared successfully." });
    }
}