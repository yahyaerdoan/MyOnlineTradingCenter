using Microsoft.AspNetCore.SignalR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;
using MyOnlineTradingCenter.SignalRLayer.Concretions.Constants;
using MyOnlineTradingCenter.SignalRLayer.Concretions.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.SignalRLayer.Concretions.HubServices;

public class ProductHubService : IProductHubService
{
    private readonly IHubContext<ProductHub> _hubContext;

    public ProductHubService(IHubContext<ProductHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task ProductAddedMessageAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync(ReceivedFunctionName.ProductAddedMessage, message);
    }
}
