using Microsoft.Extensions.DependencyInjection;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Extensions;

public static class ServiceRegistration
{
    public static void AddInfrastructureServiceRegistrations(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}
