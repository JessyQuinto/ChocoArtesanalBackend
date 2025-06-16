using AutoMapper;
using ChocoArtesanal.Application.Interfaces;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChocoArtesanal.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducersController : ControllerBase
{
    private readonly IProducerRepository _producerRepository;
    private readonly IMapper _mapper;

    public ProducersController(IProducerRepository producerRepository, IMapper mapper)
    {
        _producerRepository = producerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProducerDto>>> GetProducers()
    {
        var producers = await _producerRepository.GetAllAsync();
        var producerDtos = _mapper.Map<IEnumerable<ProducerDto>>(producers);
        return Ok(producerDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProducerDto>> GetProducer(int id)
    {
        var producer = await _producerRepository.GetByIdAsync(id);
        if (producer == null)
        {
            return NotFound();
        }
        var producerDto = _mapper.Map<ProducerDto>(producer);
        return Ok(producerDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProducerDto>> CreateProducer([FromBody] CreateProducerDto createProducerDto)
    {
        var producer = _mapper.Map<Producer>(createProducerDto);
        await _producerRepository.AddAsync(producer);
        await _producerRepository.SaveChangesAsync();

        var producerDto = _mapper.Map<ProducerDto>(producer);
        return CreatedAtAction(nameof(GetProducer), new { id = producerDto.Id }, producerDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProducer(int id, [FromBody] UpdateProducerDto updateProducerDto)
    {
        var producer = await _producerRepository.GetByIdAsync(id);
        if (producer == null)
        {
            return NotFound();
        }

        _mapper.Map(updateProducerDto, producer);
        _producerRepository.Update(producer);
        await _producerRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProducer(int id)
    {
        var producer = await _producerRepository.GetByIdAsync(id);
        if (producer == null)
        {
            return NotFound();
        }

        _producerRepository.Delete(producer);
        await _producerRepository.SaveChangesAsync();

        return NoContent();
    }
}