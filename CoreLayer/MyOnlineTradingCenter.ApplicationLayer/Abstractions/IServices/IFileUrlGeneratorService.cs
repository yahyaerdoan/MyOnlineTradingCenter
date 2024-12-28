using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IFileUrlGeneratorService
{
    Response<string> GenerateFileUrl(string fileName);
}
