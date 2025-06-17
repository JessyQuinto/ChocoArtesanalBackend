namespace ChocoArtesanal.Application.Dtos;

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class SaveCartDto
{
    public List<CartItemDto> Items { get; set; } = new();
}