using System;

namespace CustomerOrders.API.Models
{
    public interface IAuditable
    {
        DateTime DataRegister { get; set; }
        DateTime? DataUpdate { get; set; }
    }
}


