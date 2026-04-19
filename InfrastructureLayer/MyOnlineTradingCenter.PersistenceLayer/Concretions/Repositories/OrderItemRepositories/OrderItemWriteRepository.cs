using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderItemRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.OrderItemRepositories;

public class OrderItemWriteRepository : WriteRepository<OrderItem>, IOrderItemWriteRepository
{
    public OrderItemWriteRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
