namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface ICompletedOrderService
{
    Task<bool> CompleteOrderAsync(string orderId);
}
