using System;

namespace CustomerOrders.API.DTOs.Customer
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataRegister { get; set; }
        public DateTime? DataUpdate { get; set; }
    }
}


