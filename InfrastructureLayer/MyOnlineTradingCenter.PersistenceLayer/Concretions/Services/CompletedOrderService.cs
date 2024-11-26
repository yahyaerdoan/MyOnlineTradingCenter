using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICompletedOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class CompletedOrderService : ICompletedOrderService
{
    private readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;

    public CompletedOrderService(ICompletedOrderWriteRepository completedOrderWriteRepository, IOrderReadRepository orderReadRepository)
    {
        _completedOrderWriteRepository = completedOrderWriteRepository;
        _orderReadRepository = orderReadRepository;
    }

    public async Task<bool> CompleteOrderAsync(string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId)) return false;

        if (!Guid.TryParse(orderId, out Guid parsedOrderId)) return false;

        var order = await _orderReadRepository.GetByIdAsync(orderId);
        if (order == null) return false;

        var completedOrder = new CompletedOrder { OrderId = parsedOrderId };

        await _completedOrderWriteRepository.AddAsync(completedOrder);
        await _completedOrderWriteRepository.SaveAsync();

        return true;
    }
}
