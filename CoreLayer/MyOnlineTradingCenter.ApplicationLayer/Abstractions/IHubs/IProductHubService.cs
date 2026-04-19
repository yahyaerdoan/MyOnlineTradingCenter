namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;

public interface IProductHubService
{
    Task ProductAddedMessageAsync(string message);
}
