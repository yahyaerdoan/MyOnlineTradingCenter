using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IBasketRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.BasketRepositories;

public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
{
    public BasketReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
