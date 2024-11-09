using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.Get;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.GetByIdDetail;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using ResultHandler.Interfaces.Contracts;
using IResult = ResultHandler.Interfaces.Contracts.IResult;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderService _orderService;

        public OrdersController(IMediator mediator, IOrderService orderService)
        {
            _mediator = mediator;
            _orderService = orderService;
        }

        [HttpPost]       
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest request)
        {
            IResult response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] Pagination pagination)
        {
            var request = new GetOrdersQueryRequest(pagination);
            IResult response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdOrderDetail([FromQuery] GetByIdOrderDetailQueryRequest request)
        {
            IDataResult<GetByIdOrderDetailQueryResponse?> response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
