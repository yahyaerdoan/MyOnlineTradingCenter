using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorages.IAzureStorages;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.AzureStorages;

public class AzureStorage : IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient? _blobContainerClient;
    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new(configuration["Storages:AzureCloudStorage"]);
    }

    public async Task DeleteAsync(string targetFolderPathOrContainerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(targetFolderPathOrContainerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public async Task<List<string>> GetFiles(string targetFolderPathOrContainerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(targetFolderPathOrContainerName);
        var blobs = _blobContainerClient.GetBlobs().Select(blob => blob.Name).ToList();
        return await Task.FromResult(blobs);
    }

    public bool HasFile(string targetFolderPathOrContainerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(targetFolderPathOrContainerName);
        return _blobContainerClient.GetBlobs().Any(blob => blob.Name == fileName);
    }

    public async Task<List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)>> UploadAsync(string targetFolderPathOrContainerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(targetFolderPathOrContainerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        var values = new List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)>();
        foreach (IFormFile file in files)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(file.Name);
            await blobClient.UploadAsync(file.OpenReadStream());

            var fullPath = blobClient.Uri.ToString();
            var fileExtensions = Path.GetExtension(file.FileName);

            values.Add((file.FileName, fileExtensions, fullPath, targetFolderPathOrContainerName));
        }
        return values;
    }
}
