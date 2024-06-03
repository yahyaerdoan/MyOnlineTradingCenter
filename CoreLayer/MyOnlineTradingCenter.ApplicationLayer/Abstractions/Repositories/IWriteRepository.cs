using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T entity);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> datas);
        bool Update(T entity);
        bool UpdateRange(List<T> datas);
        Task<int> SaveAsync();
    }
}
