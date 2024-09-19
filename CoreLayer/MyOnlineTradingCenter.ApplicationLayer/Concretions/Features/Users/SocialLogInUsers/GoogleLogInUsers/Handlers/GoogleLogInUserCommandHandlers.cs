using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users.GoogleLogInUsers;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Handlers;

public class GoogleLogInUserCommandHandlers : IRequestHandler<GoogleLogInUserCommandRequest, Response<GoogleLogInUserCommandResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly IAuthService _authService;

    public GoogleLogInUserCommandHandlers(UserManager<User> userManager, ITokenHandler tokenHandler, IAuthService authService)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _authService = authService;
    }

    public async Task<Response<GoogleLogInUserCommandResponse>> Handle(GoogleLogInUserCommandRequest request, CancellationToken cancellationToken)
    {        
        Response<GoogleLogInUserCommandResponse> response = await _authService.GoogleLogInAsync(request);
        //string accessToken = response?.Data?.Token?.AccessToken ?? string.Empty;
        //Token token = new() { AccessToken = accessToken };
        //return new(){ Message = response?.Message ?? "Error!", Succeeded = response?.IsSuccessful ?? false, Token = token};
        return response;

        #region Code refactored.
        //var settings = new GoogleJsonWebSignature.ValidationSettings()
        //{
        //    Audience = new List<string> { "554001534100-n61ikte483p3maaeo3drs07p1eph7inu.apps.googleusercontent.com" }
        //};
        //var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

        //var userInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
        //User user = await _userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey);

        //bool result = user != null;
        //var response = new GoogleLogInUserCommandResponse();

        //if (user == null)
        //{
        //    user = await _userManager.FindByEmailAsync(payload.Email);
        //    if (user == null)
        //    {
        //        user = new()
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Email = payload.Email,
        //            UserName = payload.GivenName,
        //            FirtName = payload.GivenName,
        //            LastName = payload.FamilyName,
        //        };
        //        var identityResult = await _userManager.CreateAsync(user);
        //        result = identityResult.Succeeded;
        //    }
        //}
        //if (result)
        //{
        //    await _userManager.AddLoginAsync(user, userInfo);
        //    Token token = _tokenHandler.CreateAccessToken(5);
        //    response.Token = token;
        //    response.Succeeded = true;
        //    response.Message = "User logged in successfully with Google!";
        //}      

        //else
        //    response.Message = "Invalid credentials!";
        //return response;
        #endregion
    }
}
