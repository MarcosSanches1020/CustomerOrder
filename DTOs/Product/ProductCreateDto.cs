using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrders.API.DTOs.Product;

public class ProductCreateDto
{

    [Required(ErrorMessage = "O Nome do produto é obigatório")]
    [MaxLength(50, ErrorMessage = "O nome do produto deve conter menos que 50 characters")]
    [Column("NAME_PRODUCT")]
    public string NameProduct { get; set; } = string.Empty;

    [Required(ErrorMessage = "O preço do produto é obigatório")]
    [Column("PRICE_PRODUCT", TypeName = "decimal(18,2)")]
    public decimal PriceProduct { get; set; }

    [Required(ErrorMessage = "O tipo é obigatório")]
    [MaxLength(50, ErrorMessage = "O o tipo do produto deve conter menos que 11 characters")]
    [Column("TYPE_PRODUCT")]
    public string TypeProduct { get; set; } = string.Empty;

    [MaxLength(150, ErrorMessage = "O o tipo do produto deve conter menos que 150 characters")]
    [Column("DESCRIPTION")]
    public string Description { get; set; } = string.Empty;

}
