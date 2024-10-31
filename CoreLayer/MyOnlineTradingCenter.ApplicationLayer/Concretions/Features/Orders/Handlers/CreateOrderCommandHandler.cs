using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Constants;
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
        if (string.IsNullOrWhiteSpace(request.CreateOrderDto.Address) || string.IsNullOrWhiteSpace(request.CreateOrderDto.Description))
        return new ErrorResult(OrderMessages.PropertiesCannotBeEmpty, HttpStatusCode.BadRequest);

        UserDto currentUser = await _userService.GetCurrentUserAsync();
        if (currentUser == null)
        return new ErrorResult(OrderMessages.UserMustBeLoggedIn, HttpStatusCode.Unauthorized);

        BasketDto basketDto = await _basketService.GetBasketByUserIdAsync(currentUser.Id);
        if (basketDto == null || !basketDto.Items.Any())
        return new ErrorResult(OrderMessages.NoItemsInBasket, HttpStatusCode.BadRequest);

        bool orderCreated = await _orderService.CreateOrderAsync(request.CreateOrderDto);
        if (!orderCreated)
        return new ErrorResult(OrderMessages.CreatingFailed, HttpStatusCode.InternalServerError);

        await _orderHubService.OrderAddedMessageAsync(OrderMessages.NewOrderCreated);
        return new SuccessResult(OrderMessages.OrderCreated, HttpStatusCode.Created);    
    }
}
