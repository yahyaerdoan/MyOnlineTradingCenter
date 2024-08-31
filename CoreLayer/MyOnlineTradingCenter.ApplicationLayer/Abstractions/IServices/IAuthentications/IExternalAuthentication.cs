using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users.GoogleLogInUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices.IAuthentications;

public interface IExternalAuthentication
{
    Task<Response<GoogleLogInUserCommandResponseDto>> GoogleLogInAsync(GoogleLogInUserCommandRequestDto requestDto);
    Task FacebookLogInAsync();
    Task InstagramLogInAsync();
    Task GithubLogInAsync();
    Task LinkedInLogInAsync();   
}
