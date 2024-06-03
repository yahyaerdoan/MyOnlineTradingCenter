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

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories
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

        public async Task<bool> RemoveAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Task.Run( ()=> Table.Remove(entity));
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveByIdAsync(string id)
        {
           T entity = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            return await RemoveAsync(entity);
        }

        public bool RemoveRange(List<T> datas)
        {
            Table.AddRange(datas);
            return true;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public async Task<bool> UpdateAsync(T entity)
        {
            EntityEntry entityEntry = await Task.Run( ()=> Table.Update(entity));
            return entityEntry.State == EntityState.Modified;
        }

        public bool UpdateRange(List<T> datas)
        {
            Table.UpdateRange(datas); 
            return true;            
        }
    }
}
