using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrders.API.Models;

[Table("CART_ITEM")]
public class CartItem : IAuditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CART_ID")]
    public int CartId { get; set; }

    [Column("CART")]
    public Cart Cart { get; set; }

    [Column("PRODUCT_ID")]
    public int ProductId { get; set; }

    public Products Product { get; set; }

    [Column("QUANTITY")]
    public int Quantity { get; set; }

    [Required]
    [Column("DATA_REGISTER")]
    public DateTime DataRegister { get; set; }

    [Column("DATA_UPDATE")]
    public DateTime? DataUpdate { get; set; }
}
