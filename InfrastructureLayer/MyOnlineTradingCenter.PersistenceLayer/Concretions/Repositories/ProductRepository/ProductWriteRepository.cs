using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.Repositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.ProductRepository
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
        {
        }
    }
}
