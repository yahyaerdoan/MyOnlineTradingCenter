using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Baskets;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;
using System.Net;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, IResult>
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IBasketService _basketService;
    private readonly IOrderHubService _orderHubService;

    public CreateOrderCommandHandler(IOrderService orderService, IUserService userService, IBasketService basketService, IOrderHubService orderHubService)
    {
        _orderService = orderService;
        _userService = userService;
        _basketService = basketService;
        _orderHubService = orderHubService;
    }

    public async Task<IResult> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Address) || string.IsNullOrWhiteSpace(request.Description))
        return new ErrorResult("Address and description cannot be empty.", HttpStatusCode.BadRequest);

        UserDto currentUser = await _userService.GetCurrentUserAsync();
        if (currentUser == null)
        return new ErrorResult("User must be logged in to create an order.", HttpStatusCode.Unauthorized);

        BasketDto basketDto = await _basketService.GetBasketByUserIdAsync(currentUser.Id);
        if (basketDto == null || !basketDto.Items.Any())
        return new ErrorResult("No items in basket to create an order.", HttpStatusCode.BadRequest);

        bool orderCreated = await _orderService.CreateOrderAsync(request.CreateOrderDto);
        if (!orderCreated)
        return new ErrorResult("Creating order failed.", HttpStatusCode.InternalServerError);

        await _orderHubService.OrderAddedMessageAsync("New order created successfully.");
        return new SuccessResult("Order created successfully.", HttpStatusCode.Created);    
    }
}
