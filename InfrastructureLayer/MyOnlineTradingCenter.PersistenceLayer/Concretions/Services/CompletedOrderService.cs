using Microsoft.EntityFrameworkCore;
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
    private readonly IEmailService _emailService;

    public CompletedOrderService(ICompletedOrderWriteRepository completedOrderWriteRepository, IOrderReadRepository orderReadRepository, IEmailService emailService)
    {
        _completedOrderWriteRepository = completedOrderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _emailService = emailService;
    }

    public async Task<bool> CompleteOrderAsync(string orderId)
    {
        var order = await _orderReadRepository.GetWhere(x => x.Id == Guid.Parse(orderId.ToString())).Select(x => new
        {
            x.OrderNumber,
            x.CreatedDate,
            UserEmail = x.User.Email,
            UserFullName = x.User.FirstName + " " + x.User.LastName,
        }).FirstOrDefaultAsync();

        if (order == null || string.IsNullOrEmpty(order.UserEmail)) return false;

        var completedOrderDto = new CompleteOrderDto { OrderId = orderId };
        var completedOrder = new CompletedOrder() { OrderId = Guid.Parse(completedOrderDto.OrderId), Status = true };

        await _completedOrderWriteRepository.AddAsync(completedOrder);
        var savedResult = await _completedOrderWriteRepository.SaveAsync();

        await _emailService.SendCompletedOrderAsync(order.UserEmail, order.OrderNumber, order.CreatedDate, order.UserFullName);

        return savedResult > 0;
    }
}
