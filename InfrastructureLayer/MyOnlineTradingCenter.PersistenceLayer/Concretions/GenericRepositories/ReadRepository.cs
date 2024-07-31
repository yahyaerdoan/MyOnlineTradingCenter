using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly MyOnlineTradingCenterPostgreSqlDbContext _context;

        public ReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool traking = true)
        {
            var query = Table.AsQueryable();
            if (!traking)
                query = query.AsNoTracking();
            return query;
        }  

        public async Task<T> GetByIdAsync(string id, bool traking = true)
        //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if (!traking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool traking = true)
        { 
            var query = Table.AsQueryable();
            if(!traking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool traking = true)
        {
            var query = Table.Where(expression);
            if(!traking) 
                query = query.AsNoTracking();
            return query;
        }
    }
}
