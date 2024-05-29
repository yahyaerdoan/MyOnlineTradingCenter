using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<MyOnlineTradingCenterPostgreSqlDbContext>
    {
        public MyOnlineTradingCenterPostgreSqlDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyOnlineTradingCenterPostgreSqlDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=MyOnlineTradingCenterDb; Username=postgres; Password=12345");
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
