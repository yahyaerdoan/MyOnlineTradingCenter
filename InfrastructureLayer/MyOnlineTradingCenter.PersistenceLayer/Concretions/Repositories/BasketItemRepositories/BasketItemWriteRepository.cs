using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IBasketItemRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.BasketItemRepositories;

public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
{
    public BasketItemWriteRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
