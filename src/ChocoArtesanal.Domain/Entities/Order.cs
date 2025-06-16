namespace ChocoArtesanal.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string Status { get; set; } = "pending";
    public decimal Total { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string PaymentStatus { get; set; } = "pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public User? User { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new();
}