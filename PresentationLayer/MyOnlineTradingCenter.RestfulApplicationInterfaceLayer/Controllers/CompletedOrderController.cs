using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.CompletedOrders.Commands.Create;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompletedOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CompleteOrderAsync([FromBody] CompleteOrderCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
