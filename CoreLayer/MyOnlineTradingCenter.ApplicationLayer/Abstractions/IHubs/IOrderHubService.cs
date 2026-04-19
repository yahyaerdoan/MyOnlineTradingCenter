namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;

public interface IOrderHubService
{
    Task OrderAddedMessageAsync(string message);
}
