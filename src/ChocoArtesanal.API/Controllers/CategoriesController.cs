using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChocoArtesanal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryRepository categoryRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categories = await categoryRepository.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<CategoryDto>>(categories));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(mapper.Map<CategoryDto>(category));
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<CategoryDto>> GetBySlug(string slug)
    {
        var category = await categoryRepository.GetBySlugAsync(slug);
        if (category == null) return NotFound();
        return Ok(mapper.Map<CategoryDto>(category));
    }
}