using CustomerOrders.API.DTOs.Product;
using CustomerOrders.API.Models;

namespace CustomerOrders.API.Mappings;

public static class ProductMappingExtensions
{

    public static Products ToEntityProduct(this ProductCreateDto dto)
    {
        return new Products
        {
            NameProduct = dto.NameProduct,
            PriceProduct = dto.PriceProduct,
            TypeProduct = dto.TypeProduct,
            Description = dto.Description
        };
    }

    public static void ApplyToEntity(this ProductUpdateDto dto, Products entity)
    {
        {
            entity.NameProduct = dto.NameProduct;
            entity.PriceProduct = dto.PriceProduct;
            entity.TypeProduct = dto.TypeProduct;
            entity.Description = dto.Description;
        }
        ;
    }

    public static ProductResponseDto ToResponseProductDto(this Products entity)
    {
        return new ProductResponseDto
        {
            Id = entity.id,
            NameProduct = entity.NameProduct,
            PriceProduct = entity.PriceProduct,
            TypeProduct = entity.TypeProduct,
            Description = entity.Description,
            DataRegister = entity.DataRegister,
            DataUpdate = entity.DataUpdate
        };
    }

}
