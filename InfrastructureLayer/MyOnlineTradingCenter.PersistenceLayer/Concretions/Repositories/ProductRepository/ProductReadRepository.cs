using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.ProductRepository
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
        {
        }
    }
}
