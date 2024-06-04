using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICustomerRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.CustomerRepository
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
        {
        }
    }
}
