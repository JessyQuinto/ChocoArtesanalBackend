using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChocoArtesanal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository, IMapper mapper) : ControllerBase
{    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] int? categoryId, [FromQuery] string? search)
    {
        var products = await productRepository.GetAllAsync(categoryId, search);
        return Ok(mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(mapper.Map<ProductDto>(product));
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ProductDto>> GetBySlug(string slug)
    {
        var product = await productRepository.GetBySlugAsync(slug);
        if (product == null) return NotFound();
        return Ok(mapper.Map<ProductDto>(product));
    }

    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetFeatured()
    {
        var products = await productRepository.GetFeaturedAsync();
        return Ok(mapper.Map<IEnumerable<ProductDto>>(products));
    }
}