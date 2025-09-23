using CustomerOrders.API.DTOs.Customer;
using CustomerOrders.API.Models;

namespace CustomerOrders.API.Mappings
{
    public static class CustomerMappingExtensions
    {
        public static Customer ToEntity(this CustomerCreateDto dto)
        {
            return new Customer
            {
                Name = dto.Name,
                Cpf = dto.Cpf
            };
        }

        public static void ApplyToEntity(this CustomerUpdateDto dto, Customer entity)
        {
            entity.Name = dto.Name;
            entity.Cpf = dto.Cpf;
        }

        public static CustomerResponseDto ToResponseDto(this Customer entity)
        {
            return new CustomerResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Cpf = entity.Cpf,
                DataRegister = entity.DataRegister.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                DataUpdate = entity.DataUpdate.HasValue ? entity.DataUpdate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ") : null
            };
        }
    }
}


