using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.Commands.Create;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommandRequest request)
    {
        CreateUserCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}
