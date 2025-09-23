using System.Collections.Generic;

namespace CustomerOrders.API.DTOs.Cart;

public class CartItemResponseDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}

public class CartResponseDto
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<CartItemResponseDto> Items { get; set; } = new();
    public decimal TotalCart { get; set; }
}
