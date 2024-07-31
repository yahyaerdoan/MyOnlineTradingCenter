using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> datas);
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveByIdAsync(string id);
        bool RemoveRange(List<T> datas);
        Task<bool> UpdateAsync(T entity);
        bool UpdateRange(List<T> datas);
        Task<int> SaveAsync();
    }
}
