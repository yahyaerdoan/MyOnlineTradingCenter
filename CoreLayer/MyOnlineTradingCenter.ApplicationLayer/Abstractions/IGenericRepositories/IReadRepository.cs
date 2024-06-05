﻿using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool traking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool traking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool traking = true);
        Task<T> GetByIdAsync(string id, bool traking = true);
    }
}
