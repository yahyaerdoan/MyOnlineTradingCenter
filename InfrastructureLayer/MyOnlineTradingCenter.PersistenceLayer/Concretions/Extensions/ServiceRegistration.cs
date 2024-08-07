﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICustomerRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IImageFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IInvoiceFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IUploadedFileRepositories;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts.Configurations;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.CustomerRepository;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.ImageFileRepository;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.InvoiceFileRepository;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.OrderRepository;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.ProductRepository;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.UploadedFileRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServiceRegistrations(this IServiceCollection services)
        {
            services.AddDbContext<MyOnlineTradingCenterPostgreSqlDbContext>(optionsAction: options => options
            .UseNpgsql(ConnectionStringConfiguration.ConnectionString), ServiceLifetime.Scoped);

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IImageFileReadRepository, ImageFileReadRepository>();
            services.AddScoped<IImageFileWriteRepository, ImageFileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped<IUploadedFileReadRepository, UploadedFileReadRepository>();
            services.AddScoped<IUploadedFileWriteRepository, UploadedFileWriteRepository>();
        }
    }
}
 