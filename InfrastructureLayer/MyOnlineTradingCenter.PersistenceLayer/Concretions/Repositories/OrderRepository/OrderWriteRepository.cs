using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.OrderRepository
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
        {
        }
    }
}
