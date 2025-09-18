using System.ComponentModel.DataAnnotations;

namespace CustomerOrders.API.DTOs.Customer
{
    public class CustomerUpdateDto
    {
        [Required(ErrorMessage = "O Nome é obigatório")]
        [MaxLength(50, ErrorMessage = "O nome deve conter menos que 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O cpf é obigatório")]
        [MaxLength(11, ErrorMessage = "O cpf deve conter menos que 11 characters")]
        public string Cpf { get; set; } = string.Empty;
    }
}


