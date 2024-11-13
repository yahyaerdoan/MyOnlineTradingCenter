using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.ResetPasswords.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.VerifyResetTokens.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn(LogInUserCommandRequest request)
        {
            Response<LogInUserCommandResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GoogleLogIn(GoogleLogInUserCommandRequest request)
        {
            Response<GoogleLogInUserCommandResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogIn([FromBody] RefreshTokenLogInCommandRequest request)
        {
            Response<RefreshTokenLogInCommandResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommandRequest request)
        {
            ResetPasswordCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("VerifyResetToken")]
        public async Task<IActionResult> VerifyResetTokenAsync([FromBody] VerifyResetTokenCommandRequest request)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
