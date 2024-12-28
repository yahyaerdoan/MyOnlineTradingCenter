using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorages.ILocalStorages;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Utilities.FileHelpers;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.LocalStorages
{
    public class LocalStorage : FileNameHelper, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string targetFolderPathOrContainerName, string fileName)
        {
            var filePath = $"{targetFolderPathOrContainerName}\\{fileName}";
            if (File.Exists(filePath))
                File.Delete(filePath);
            await Task.CompletedTask;
        }

        public Task<List<string>> GetFiles(string targetFolderPathOrContainerName)
        {
            DirectoryInfo directoryInfo = new(targetFolderPathOrContainerName);
            List<string> fileNames = directoryInfo.GetFiles().Select(x => x.Name).ToList();
            return Task.FromResult(fileNames);
        }

        public bool HasFile(string targetFolderPathOrContainerName, string fileName)
        {
            var filePath = $"{targetFolderPathOrContainerName}\\{fileName}";
            return File.Exists(filePath);
        }

        public async Task<List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)>> UploadAsync(string targetFolderPathOrContainerName, IFormFileCollection files)
        {
            string uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, targetFolderPathOrContainerName);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var uploadedFiles = new List<(string FileName, string FileExtension, string FullPath, string targetFolderPath)>();
            foreach (var file in files)
            {
                string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);
                string newFileName = await GenerateUniqueFileNameAsync(originalFileName, fileExtension, uploadDirectory, HasFile);
                string fullFilePath = Path.Combine(uploadDirectory, newFileName);

                bool isFileCopied = await CopyFileAsync(fullFilePath, file);
                if (isFileCopied)
                {
                    uploadedFiles.Add((newFileName, fileExtension, fullFilePath, targetFolderPathOrContainerName));
                }
            }

            return uploadedFiles;
        }

        #region Private Helper Methods, Only Using Here
        private async Task<bool> CopyFileAsync(string destinationFilePath, IFormFile sourceFile)
        {
            await using var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: false);
            await sourceFile.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }

        #region Not Using This is Updated for Architecture
        //private async Task<string> GenerateUniqueFileNameAsync(string fileName, string fileExtension, string directoryPath)
        //{
        //    return await Task.Run(() =>
        //    {
        //        string newFileName = fileName;
        //        string fullFileName = $"{newFileName}{fileExtension}";
        //        int counter = 1;

        //        while (File.Exists(Path.Combine(directoryPath, fullFileName)))
        //        {
        //            newFileName = $"{fileName}_{counter++}";
        //            fullFileName = $"{newFileName}{fileExtension}";
        //        }

        //        return fullFileName;
        //    });
        //}

        #endregion

        #endregion
    }
}
