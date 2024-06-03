using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.Repositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntity;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly MyOnlineTradingCenterPostgreSqlDbContext _context;

        public WriteRepository(MyOnlineTradingCenterPostgreSqlDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }
          

        public Task<bool> AddRangeAsync(List<T> entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
