using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;

public interface IProductHubService
{
    Task ProductAddedMessageAsync(string message);
}
