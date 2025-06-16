namespace ChocoArtesanal.Application.Dtos;

public class CreateOrderRequestDto
{
    public string ShippingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = "PSE";
    public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
}

public class CreateOrderDetailDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
}

public class OrderDetailDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty; // Para mostrar en el detalle
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}