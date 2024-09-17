using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;

public class GoogleLogInUserCommandRequest :  IRequest<GoogleLogInUserCommandResponse>
{
    public string Id { get; set; }
    public int TokenLifeTime { get; set; } = 15;
    public string IdToken { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string Provider { get; set; }
}
