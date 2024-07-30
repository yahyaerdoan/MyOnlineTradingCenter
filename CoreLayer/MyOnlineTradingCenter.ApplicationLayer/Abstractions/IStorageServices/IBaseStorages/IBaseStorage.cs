using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IBaseStorages;

public interface IBaseStorage
{
    Task<List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)>> UploadAsync(string targetFolderPathOrContainerName, IFormFileCollection files);

    Task DeleteAsync(string targetFolderPathOrContainerName, string fileName);
    Task<List<string>> GetFiles(string targetFolderPathOrContainerName);
    bool HasFile(string targetFolderPathOrContainerName, string fileName);
}
