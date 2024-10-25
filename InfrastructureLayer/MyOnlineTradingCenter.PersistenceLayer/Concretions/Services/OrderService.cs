using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IUserService _userService;

    public OrderService(IOrderWriteRepository orderWriteRepository, IUserService userService)
    {
        _orderWriteRepository = orderWriteRepository;
        _userService = userService;
    }

    public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var userId = await _userService.GetCurrentUserAsync();
        await _orderWriteRepository.AddAsync(new()
        {
            UserId = userId.Id,
            OrderNumber = "1-2-3-4-5-6-7-8-9", //createOrderDto.OrderNumber,
            Address =  "45876 W Granville Ave", //createOrderDto.Address,
            Description = "Order Description 1.", //createOrderDto.Description
            
        });
        await _orderWriteRepository.SaveAsync();
    }
}
