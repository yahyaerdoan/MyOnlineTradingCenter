using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IBaseStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;

public interface IStorageService : IBaseStorage
{
    public string StorageName { get; } 
}
