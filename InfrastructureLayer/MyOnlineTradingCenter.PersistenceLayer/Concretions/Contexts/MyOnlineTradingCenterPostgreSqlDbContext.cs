using Microsoft.EntityFrameworkCore;
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
    }
}
