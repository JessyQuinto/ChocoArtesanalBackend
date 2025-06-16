using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// NOTA: En una implementación completa, usaríamos DTOs y AutoMapper.
// Por simplicidad, este ejemplo devuelve la entidad de dominio directamente.

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        _logger.LogInformation("Obteniendo todos los productos.");
        var products = await _productRepository.GetAllAsync();
        // Aquí deberías mapear 'products' a una lista de 'ProductDto'
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        _logger.LogInformation("Obteniendo producto con ID: {ProductId}", id);
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning("Producto con ID: {ProductId} no encontrado.", id);
            return NotFound();
        }

        // Aquí deberías mapear 'product' a 'ProductDto'
        return Ok(product);
    }
}