namespace ChocoArtesanal.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User? User { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}