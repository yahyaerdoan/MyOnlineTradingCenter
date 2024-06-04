using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICustomerRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts.Configurations;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.CustomerRepository;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.OrderRepository;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<MyOnlineTradingCenterPostgreSqlDbContext>(optionsAction: options => options
            .UseNpgsql(ConnectionStringConfiguration.ConnectionString), ServiceLifetime.Scoped);

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        }
    }
}
 