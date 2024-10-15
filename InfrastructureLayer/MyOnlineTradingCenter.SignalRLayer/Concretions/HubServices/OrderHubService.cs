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

public class OrderHubService : IOrderHubService
{
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderHubService(IHubContext<OrderHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task OrderAddedMessageAsync(string message)
    {
       await _hubContext.Clients.All.SendAsync(ReceivedFunctionName.OrderaddedMessage, message);
    }
}
