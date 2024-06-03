using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddAsync(List<T> entity);
        bool Remove(T entity);
        bool Remove(string id);
        Task<bool>  UpdateAsync(T entity);
    }
}
