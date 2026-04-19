using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
