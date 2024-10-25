using Microsoft.Extensions.DependencyInjection;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IBaseStorages;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Services;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Services.Files;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Enums.StorageTypes;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.AzureStorages;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.LocalStorages;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.StorageServices;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Utilities.FileHelpers;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Extensions;

public static class ServiceRegistration
{
    public static void AddInfrastructureServiceRegistrations(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddHttpContextAccessor();
        services.AddSingleton<IFileUrlGeneratorService, FileUrlGeneratorService>();
    }

    public static void AddStorageServices<T>(this IServiceCollection services) where T : FileNameHelper, IBaseStorage
    {
        services.AddScoped<IBaseStorage, T>();
    }

    public static void AddStorageServices(this IServiceCollection services,  StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.LocalStorage:
                services.AddScoped<IBaseStorage, LocalStorage>();
                break;
            case StorageType.AzureStorage:
                services.AddScoped<IBaseStorage, AzureStorage>();
                break;
            case StorageType.FireBaseStorage:
                break;
            case StorageType.AwsStorage:
                break;
            case StorageType.GoogleCloudStorage:
                break;
            default:
                services.AddScoped<IBaseStorage, LocalStorage>();
                break;
        }
    }
}
