using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;

public class GoogleLogInUserCommandResponse
{
    //public bool Succeeded { get; set; }
    //public string Message { get; set; }
    public Token Token { get; set; }
}
