using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderItemRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.OrderItemRepositories;

public class OrderItemReadRepository : ReadRepository<OrderItem>, IOrderItemReadRepository
{
    public OrderItemReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
