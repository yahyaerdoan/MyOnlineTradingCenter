using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICompletedOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.CompletedOrders;
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
        var order = await _orderReadRepository.GetByIdAsync(orderId);
        if (order == null) return false;

        var completedOrderDto = new CompleteOrderDto { OrderId = orderId };
        var completedOrder = new CompletedOrder() { OrderId = Guid.Parse(completedOrderDto.OrderId), Status = true };

        await _completedOrderWriteRepository.AddAsync(completedOrder);
        var savedResult = await _completedOrderWriteRepository.SaveAsync();

        return savedResult > 0;
    }
}
