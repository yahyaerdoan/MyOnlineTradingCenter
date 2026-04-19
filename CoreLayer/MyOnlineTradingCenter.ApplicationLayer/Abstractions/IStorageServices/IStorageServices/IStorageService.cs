using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IBaseStorages;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;

public interface IStorageService : IBaseStorage
{
    public string StorageName { get; }
}
