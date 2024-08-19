using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Handlers;

public class GoogleLogInUserCommandHandlers : IRequestHandler<GoogleLogInUserCommandRequest, GoogleLogInUserCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenHandler _tokenHandler;

    public GoogleLogInUserCommandHandlers(UserManager<User> userManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<GoogleLogInUserCommandResponse> Handle(GoogleLogInUserCommandRequest request, CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { "554001534100-n61ikte483p3maaeo3drs07p1eph7inu.apps.googleusercontent.com" }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

        var userInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
        User user = await _userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey);

        bool result = user != null;
        var response = new GoogleLogInUserCommandResponse();

        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.GivenName,
                    FirtName = payload.GivenName,
                    LastName = payload.FamilyName,
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }
        if (result)
        {
            await _userManager.AddLoginAsync(user, userInfo);
            Token token = _tokenHandler.CreateAccessToken(5);
            response.Token = token;
            response.Succeeded = true;
            response.Message = "User logged in successfully with Google!";
        }      
            
        else
            response.Message = "Invalid credentials!";
        return response;
    }
}
