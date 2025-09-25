using CustomerOrders.API.DTOs.Sellers;
using CustomerOrders.API.Models;

namespace CustomerOrders.API.Mappings
{
    public static class SellersMappingExtensions
    {
        public static Sellers ToEntity(this SellersCreateDto dto)
        {
            return new Sellers
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Cpf = dto.Cpf,
                Password = dto.Password
            };
        }

        public static void ApplyToEntity(this SellersUpdateDto dto, Sellers entity)
        {
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.Cpf = dto.Cpf;
            entity.Password = dto.Password;
        }

        public static SellersResponseDto ToResponseDto(this Sellers entity)
        {
            return new SellersResponseDto
            {
                id = entity.Id,
                Name = entity.Name,
                Cpf = entity.Cpf,
                DataRegister = entity.DataRegister.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                DataUpdate = entity.DataUpdate.HasValue ? entity.DataUpdate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ") : null
            };
        }
    }
}