using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;
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
            services.AddDbContext<MyOnlineTradingCenterPostgreSqlDbContext>(optionsAction: options => options.UseNpgsql("Host=localhost; Port=5432; Database=MyOnlineTradingCenterDb; Username=postgres; Password=12345"));
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
