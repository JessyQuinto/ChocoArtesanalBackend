namespace ChocoArtesanal.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }

    // Propiedad agregada para corregir el error
    public DateTime CreatedAt { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}