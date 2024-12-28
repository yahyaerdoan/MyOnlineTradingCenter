using Microsoft.AspNetCore.Http;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IFileService
{
    Task<List<(string FileName, string FileExtension, string FullPath, string TargetFolderPath)>> UploadAsync(string targetFolderPath, IFormFileCollection files);
    Task<bool> CopyFileAsync(string fullPath, IFormFile file);
}
