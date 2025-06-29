﻿namespace ChocoArtesanal.Application.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public string Image { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public int CategoryId { get; set; }
    public int ProducerId { get; set; }
    public int Stock { get; set; }
    public bool Featured { get; set; }
    public decimal? Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string ProducerName { get; set; } = string.Empty;
}