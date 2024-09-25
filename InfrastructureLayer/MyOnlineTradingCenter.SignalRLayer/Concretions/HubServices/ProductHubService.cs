using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.SignalRLayer.Concretions.HubServices;

public class ProductHubService : IProductHubService
{
    public Task ProductAddedMessageAsync(string message)
    {
        throw new NotImplementedException();
    }
}
