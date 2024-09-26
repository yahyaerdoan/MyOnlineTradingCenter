using Microsoft.AspNetCore.Builder;
using MyOnlineTradingCenter.SignalRLayer.Concretions.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.SignalRLayer.Concretions.Extensions;

public static class HubRegistration
{
    public static void AddMapHubRegistrations(this WebApplication webApplication)
    {
        webApplication.MapHub<ProductHub>("/products-hub");
    }
}
