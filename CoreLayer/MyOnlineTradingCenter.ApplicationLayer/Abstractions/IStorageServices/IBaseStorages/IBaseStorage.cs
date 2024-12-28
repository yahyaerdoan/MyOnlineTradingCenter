using Microsoft.AspNetCore.Http;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IBaseStorages;

public interface IBaseStorage
{
    Task<List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)>> UploadAsync(string targetFolderPathOrContainerName, IFormFileCollection files);

    Task DeleteAsync(string targetFolderPathOrContainerName, string fileName);
    Task<List<string>> GetFiles(string targetFolderPathOrContainerName);
    bool HasFile(string targetFolderPathOrContainerName, string fileName);
}
