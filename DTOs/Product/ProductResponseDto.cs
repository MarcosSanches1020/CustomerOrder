using System;

namespace CustomerOrders.API.DTOs.Product;

public class ProductResponseDto
{
    public int Id { get; set; }
    public string NameProduct { get; set; } = string.Empty;
    public decimal PriceProduct { get; set; }
    public string TypeProduct { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DataRegister { get; set; } = string.Empty;
    public string DataUpdate { get; set; }

}
