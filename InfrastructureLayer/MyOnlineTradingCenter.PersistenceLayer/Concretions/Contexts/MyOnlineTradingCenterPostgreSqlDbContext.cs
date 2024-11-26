using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts
{
    public class MyOnlineTradingCenterPostgreSqlDbContext : IdentityDbContext<User, Role, string> //DbContext
    {
        public MyOnlineTradingCenterPostgreSqlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<UploadedFile> UploadedFiles { get; set; } = default!;
        public DbSet<ImageFile> ImageFiles { get; set; } = default!;
        public DbSet<InvoiceFile> InvoiceFiles { get; set; } = default!;
        public DbSet<Basket> Baskets { get; set; } = default!;
        public DbSet<BasketItem> BasketItems { get; set; } = default!;        
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<CompletedOrder> CompletedOrders { get; set; } = default!;


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var data = ChangeTracker
                .Entries<BaseEntity>()
                 .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var currentTime = DateTime.UtcNow;

            foreach (var datum in data)
            {
                _ = datum.State switch
                {
                    EntityState.Added => datum.Entity.CreatedDate = currentTime,
                    EntityState.Modified => datum.Entity.UpdatedDate = currentTime,
                    _=> DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
