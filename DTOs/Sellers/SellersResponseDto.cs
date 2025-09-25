using System;

namespace CustomerOrders.API.DTOs.Sellers;

public class SellersResponseDto
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Cpf { get; set; }
    public String DataRegister { get; set; }
    public String DataUpdate { get; set; }
}
