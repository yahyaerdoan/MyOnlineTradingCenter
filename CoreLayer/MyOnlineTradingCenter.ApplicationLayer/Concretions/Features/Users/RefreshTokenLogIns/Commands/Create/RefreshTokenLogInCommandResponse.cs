using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Commands.Create;

public class RefreshTokenLogInCommandResponse
{
    public Token Token { get; set; }
}
