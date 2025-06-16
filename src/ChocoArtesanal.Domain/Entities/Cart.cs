using System.Collections.Generic;

namespace ChocoArtesanal.Domain.Entities
{
    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public Cart(string userId)
        {
            UserId = userId;
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}