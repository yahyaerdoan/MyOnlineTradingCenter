using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<(string FileName, string FileExtension, string FullPath)>> UploadAsync(string targetFolderPath, IFormFileCollection files)
        {
            string uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, targetFolderPath);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var uploadedFiles = new List<(string FileName, string FileExtension, string FullPath)>();
            foreach (var file in files)
            {
                string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);
                string uniqueFileName = await GenerateUniqueFileNameAsync(originalFileName, fileExtension, uploadDirectory);
                string fullFilePath = Path.Combine(uploadDirectory, uniqueFileName);

                bool isFileCopied = await CopyFileAsync(fullFilePath, file);
                if (isFileCopied)
                {
                    uploadedFiles.Add((uniqueFileName, fileExtension, fullFilePath));
                }
            }

            return uploadedFiles;
        }

        public async Task<bool> CopyFileAsync(string destinationFilePath, IFormFile sourceFile)
        {
            await using var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None,4096, useAsync: false);
            await sourceFile.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }

        private async Task<string> GenerateUniqueFileNameAsync(string fileName, string fileExtension, string directoryPath)
        {
            return await Task.Run(() =>
            {
                string newFileName = fileName;
                string fullFileName = $"{newFileName}{fileExtension}";
                int counter = 1;

                while (File.Exists(Path.Combine(directoryPath, fullFileName)))
                {
                    newFileName = $"{fileName}_{counter++}";
                    fullFileName = $"{newFileName}{fileExtension}";
                }

                return fullFileName;
            });
        }
    }
}
