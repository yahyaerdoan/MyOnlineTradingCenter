using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories
{
    public interface IOrderReadRepository : IReadRepository<Order>
    {
    }
}
