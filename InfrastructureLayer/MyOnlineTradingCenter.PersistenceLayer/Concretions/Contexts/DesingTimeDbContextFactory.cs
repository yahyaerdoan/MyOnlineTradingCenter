using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts.Configurations;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<MyOnlineTradingCenterPostgreSqlDbContext>
    {
        public MyOnlineTradingCenterPostgreSqlDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyOnlineTradingCenterPostgreSqlDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(ConnectionStringConfiguration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
