using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;

public class LogInUserCommandResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
}
