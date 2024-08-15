using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;

public class LogInUserCommandRequest : IRequest<LogInUserCommandResponse>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}
