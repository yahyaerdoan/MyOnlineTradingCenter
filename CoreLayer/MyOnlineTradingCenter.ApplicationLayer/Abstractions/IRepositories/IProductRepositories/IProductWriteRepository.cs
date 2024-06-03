using MyOnlineTradingCenter.ApplicationLayer.Abstractions.Repositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories
{
    public interface IProductWriteRepository : IWriteRepository<Product>
    {
    }
}
