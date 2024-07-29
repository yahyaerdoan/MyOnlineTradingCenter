﻿using Microsoft.AspNetCore.Http;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorages;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.StorageServices;

public class StorageService : IStorageService
{
    private readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public string StorageName => _storage.GetType().Name;

    public async Task DeleteAsync(string targetFolderPathOrContainerName, string fileName)
        => await _storage.DeleteAsync(targetFolderPathOrContainerName, fileName);
    public async Task<List<string>> GetFiles(string targetFolderPathOrContainerName)
        => await _storage.GetFiles(targetFolderPathOrContainerName);

    public bool HasFile(string targetFolderPathOrContainerName, string fileName)
        => _storage.HasFile(targetFolderPathOrContainerName, fileName);

    public Task<List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)>> UploadAsync(string targetFolderPathOrContainerName, IFormFileCollection files)
         => _storage.UploadAsync(targetFolderPathOrContainerName, files);
}
