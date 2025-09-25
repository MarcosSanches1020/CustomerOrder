using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerOrders.API.DTOs.Sellers;

public class SellersUpdateDto
{
    [Required(ErrorMessage = "The field NAME is required")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field EMAIL is required")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field PHONE is required")]
    [MaxLength(15)]
    public string Phone { get; set; }

    [Required(ErrorMessage = "The field CPF is required")]
    [MaxLength(11)]
    public string Cpf { get; set; }

    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}
