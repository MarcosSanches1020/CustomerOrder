using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrders.API.Models;

[Table("CART")]
public class Cart : IAuditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CUSTOMER_ID")]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<CartItem> Items { get; set; } = new();

    [Required]
    [Column("DATA_REGISTER")]
    public DateTime DataRegister { get; set; }

    [Column("DATA_UPDATE")]
    public DateTime? DataUpdate { get; set; }
}