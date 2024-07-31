using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Utilities.FileHelpers;

public class FileNameHelper
{
    protected delegate bool HasFile(string targetFolderPathOrContainerName, string fileName);
    protected async Task<string> GenerateUniqueFileNameAsync(string fileName, string fileExtension, string targetFolderPathOrContainerName, HasFile hasFile)
    {
        fileName = NameOperation.CharacterRegulatory(fileName);
        return await Task.Run(() =>
        {
            string newFileName = fileName;
            string fullFileName = $"{newFileName}{fileExtension}";
            int counter = 2;

            while (hasFile(targetFolderPathOrContainerName, fullFileName))
            {
                newFileName = $"{fileName}-({counter++})";
                fullFileName = $"{newFileName}{fileExtension}";
            }

            return fullFileName;
        });
    }
}
