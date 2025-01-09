using Microsoft.AspNetCore.Builder;
using MyOnlineTradingCenter.SignalRLayer.Concretions.Hubs;

namespace MyOnlineTradingCenter.SignalRLayer.Concretions.Extensions;

public static class HubRegistration
{
    public static void AddMapHubRegistrations(this WebApplication webApplication)
    {
        webApplication.MapHub<ProductHub>("/products-hub");
        webApplication.MapHub<OrderHub>("/orders-hub");
    }
}
