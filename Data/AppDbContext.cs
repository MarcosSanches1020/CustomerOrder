using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }

    }
}