using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;

public class GoogleLogInUserCommandResponse
{
    //public bool Succeeded { get; set; }
    //public string Message { get; set; }
    public Token Token { get; set; }
}
