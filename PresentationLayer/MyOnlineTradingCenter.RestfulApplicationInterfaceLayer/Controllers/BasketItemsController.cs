using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Delete;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Queries.Get;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class BasketItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderService _orderService;

    public BasketItemsController(IMediator mediator, IOrderService orderService)
    {
        _mediator = mediator;
        _orderService = orderService;
    }
    [HttpGet]
    public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest request)
    {
        List<GetBasketItemsQueryResponse> response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddBasketItem(AddBasketItemCommandRequest request)
    {
       AddBasketItemCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBasketItemQuantity(UpdateBasketItemCommandRequest request)
    {
        UpdateBasketItemCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{BasketItemId}")]
    public async Task<IActionResult> DeleteBasketItemQuantity([FromRoute] DeleteBasketItemCommandRequest request)
    {
        DeleteBasketItemCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}
