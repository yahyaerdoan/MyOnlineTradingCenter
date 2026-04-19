using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories
{
    public interface IOrderWriteRepository : IWriteRepository<Order>
    {
    }
}
