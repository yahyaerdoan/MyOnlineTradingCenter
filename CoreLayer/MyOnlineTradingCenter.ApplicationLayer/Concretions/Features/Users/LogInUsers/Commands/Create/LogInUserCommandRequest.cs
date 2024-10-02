using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;

public class LogInUserCommandRequest : IRequest<Response<LogInUserCommandResponse>>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
    //public int AccessTokenLifeTime { get; set; } = 30;
}
