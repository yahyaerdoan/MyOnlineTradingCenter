using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;

public interface IStorageService : IStorage
{
    public string StorageName { get; } 
}
