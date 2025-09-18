using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerOrders.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }

        public override int SaveChanges()
        {
            ApplyTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyTimestamps()
        {
            var utcNow = DateTime.UtcNow;

            foreach (EntityEntry entry in ChangeTracker.Entries().Where(e => e.Entity is IAuditable))
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.DataRegister = utcNow;
                    entity.DataUpdate = null;

                    entry.Property(nameof(IAuditable.DataRegister)).IsModified = false;
                    entry.Property(nameof(IAuditable.DataUpdate)).IsModified = false;
                }
                else if (entry.State == EntityState.Modified)
                {

                    entry.Property(nameof(IAuditable.DataRegister)).IsModified = false;

                    entity.DataUpdate = utcNow;
                }
            }
        }
    }
}