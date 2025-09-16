using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrders.API.Models
{
    [Table("CUSTOMER")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obigatório")]
        [MaxLength(50, ErrorMessage = "O nome deve conter menos que 50 characters")]
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O cpf é obigatório")]
        [MaxLength(11, ErrorMessage = "O cpf deve conter menos que 11 characters")]
        [Column("CPF")]
        public string Cpf { get; set; } = string.Empty;
        
    }
}