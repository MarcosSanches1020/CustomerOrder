using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrders.API.Models;

[Table("SELLERS")]
public class Sellers : IAuditable
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [Required(ErrorMessage = "The field NAME is required")]
    [MaxLength(100)]
    public string Name { get; set; }= string.Empty;

    [Column("EMAIL")]
    [Required(ErrorMessage = "The field EMAIL is required")]
    [MaxLength(100)]
    public string Email { get; set; }= string.Empty;

    [Column("PHONE")]
    [Required(ErrorMessage = "The field PHONE is required")]
    [MaxLength(15)]
    public string Phone { get; set; }= string.Empty;


    [Column("CPF")]
    [Required(ErrorMessage = "The field CPF is required")]
    [MaxLength(11)]
    public string Cpf { get; set; }= string.Empty;

    [Column("PASSWORD")]
    [Required(ErrorMessage = "The field PASSWORD is required")]
    [MaxLength(100)]
    public string Password { get; set; }= string.Empty;
    
    [Required]
    [Column("DATA_REGISTER")]
    public DateTime DataRegister { get; set; }

    [Column("DATA_UPDATE")]
    public DateTime? DataUpdate { get; set; }

}