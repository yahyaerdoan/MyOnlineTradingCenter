using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Commands.Create;

public class RefreshTokenLogInCommandRequest : IRequest<Response<RefreshTokenLogInCommandResponse>>
{
    public string RefreshToken { get; set; }
}
