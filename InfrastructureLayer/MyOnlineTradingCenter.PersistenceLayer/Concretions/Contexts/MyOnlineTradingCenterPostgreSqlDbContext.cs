using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntity;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts
{
    public class MyOnlineTradingCenterPostgreSqlDbContext : DbContext
    {
        public MyOnlineTradingCenterPostgreSqlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products  { get; set; }
        public DbSet<Order> Orders  { get; set; }
        public DbSet<Customer> Customers  { get; set; }

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
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
