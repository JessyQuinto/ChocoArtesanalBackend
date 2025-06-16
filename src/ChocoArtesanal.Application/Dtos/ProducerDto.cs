namespace ChocoArtesanal.Application.Dtos;

public class ProducerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Image { get; set; }
    public bool IsFeatured { get; set; }
    public int? FoundationYear { get; set; }
}

public class CreateProducerDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Image { get; set; }
    public bool IsFeatured { get; set; }
    public int? FoundationYear { get; set; }
}

public class UpdateProducerDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Image { get; set; }
    public bool IsFeatured { get; set; }
    public int? FoundationYear { get; set; }
}