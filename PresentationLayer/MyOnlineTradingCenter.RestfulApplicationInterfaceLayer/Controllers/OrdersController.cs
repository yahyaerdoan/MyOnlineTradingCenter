using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.Get;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using IResult = ResultHandler.Interfaces.Contracts.IResult;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
