
using System.Linq;
using CustomerOrders.API.DTOs.Cart;
using CustomerOrders.API.Models;

namespace CustomerOrders.API.Mappings;

public static class CartMappingExtensions
{
    public static CartResponseDto ToResponseDto(this Cart cart)
    {
        return new CartResponseDto
        {
            CartId = cart.Id,
            CustomerId = cart.CustomerId,
            CustomerName = cart.Customer?.Name ?? string.Empty,
            Items = cart.Items.Select(item => new CartItemResponseDto
            {
                ProductId = item.ProductId,
                Name = item.Product?.NameProduct ?? string.Empty,
                Price = item.Product?.PriceProduct ?? 0,
                Quantity = item.Quantity,
                Total = item.Quantity * (item.Product?.PriceProduct ?? 0)
            }).ToList(),
            TotalCart = cart.Items.Sum(item => item.Quantity * (item.Product?.PriceProduct ?? 0))
        };
    }
}
