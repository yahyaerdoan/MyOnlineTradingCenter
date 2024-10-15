using Microsoft.Extensions.DependencyInjection;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;
using MyOnlineTradingCenter.SignalRLayer.Concretions.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.SignalRLayer.Concretions.Extensions;

public static class ServiceRegistration
{
    public static void AddSignalRServiceRegistrations(this IServiceCollection services)
    {
        services.AddTransient<IProductHubService, ProductHubService>();
        services.AddTransient<IOrderHubService, OrderHubService>();
        services.AddSignalR();
    }
}
