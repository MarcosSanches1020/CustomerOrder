using System;

namespace CustomerOrders.API.DTOs.Customer
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string DataRegister { get; set; }
        public string DataUpdate { get; set; }
    }
}


