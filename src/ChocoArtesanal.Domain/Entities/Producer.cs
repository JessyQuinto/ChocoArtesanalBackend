namespace ChocoArtesanal.Domain.Entities;

public class Producer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Image { get; set; }
    public bool Featured { get; set; }
    public int? FoundedYear { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Product> Products { get; set; } = new();
}