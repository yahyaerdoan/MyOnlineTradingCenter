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
          

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
           T entity = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            return Remove(entity);
        }

        public bool RemoveRange(List<T> datas)
        {
            Table.AddRange(datas);
            return true;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public bool Update(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public bool UpdateRange(List<T> datas)
        {
            Table.UpdateRange(datas); 
            return true;            
        }
    }
}
