using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IFileService
{
    Task<List<(string FileName, string FileExtension, string FullPath)>> UploadAsync(string targetFolderPath, IFormFileCollection files);
    Task<bool> CopyFileAsync(string fullPath, IFormFile file);

}
