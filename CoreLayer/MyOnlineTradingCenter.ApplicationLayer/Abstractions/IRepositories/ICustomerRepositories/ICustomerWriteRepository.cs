using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICustomerRepositories
{
    public interface ICustomerWriteRepository : IWriteRepository<Customer>
    {
    }
}
