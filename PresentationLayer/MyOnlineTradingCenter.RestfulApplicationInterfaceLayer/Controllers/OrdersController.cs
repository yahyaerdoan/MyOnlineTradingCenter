using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.Get;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.GetByIdDetail;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
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
        private readonly IEmailService _emailService;

        public OrdersController(IMediator mediator, IEmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
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
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetByIdOrderDetail([FromRoute] GetByIdOrderDetailQueryRequest request)
        {
            //await SendEmail();
            IDataResult<GetByIdOrderDetailQueryResponse?> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderStatusToTrueAsync([FromBody] UpdateOrderStatusToTrueCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        private async Task SendEmail()
        {
            var multipleRecipientMessage = new EmailMessage
            {
                ToMultiple = new[] { "tbatbold1999@gmail.com", "yahyaerdoan@icloud.com" },
                Subject = "Test Subject. Muiltiple Recipient",
                Body = @"
                        <html>
                            <body style='font-family: Arial, sans-serif;'>
                                <h2 style='color: #4CAF50;'>Hello Riley,</h2>
                                <p>This is a test email sent to multiple recipients separately.</p>
                                <p style='font-size: 14px;'>Thank you for using our service!</p>
                                <p>Best Regards,<br><strong>My Online Trading Center</strong></p>
                                <hr />
                                <footer style='color: #888888; font-size: 12px;'>
                                    <p>This is an automated message, please do not reply.</p>
                                </footer>
                            </body>
                        </html>",
                IsBodyHtml = true
            };
            await _emailService.SendEmailAsync(multipleRecipientMessage);
        }
    }
}
