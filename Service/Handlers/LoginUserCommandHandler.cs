using Data.Abstraction;
using Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Service.Command;
using Service.Responses;
using ServiceAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, List<AuthenticationDTOResponse>>
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserTokenService _userTokenService;
        private readonly IDapperHelper _dapperHelper;

        public LoginUserCommandHandler(IUserService userService, ITokenHandler tokenHandler, IUserTokenService userTokenService, IDapperHelper dapperHelper)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
            _userTokenService = userTokenService;
            _dapperHelper = dapperHelper;
        }

        public async Task<List<AuthenticationDTOResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.CheckLogin(request.UserName, request.Password);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var (accessToken, expiredAccess) = await _tokenHandler.CreateAccessToken(user);
                var (refreshToken, expiredRefresh) = await _tokenHandler.CreateRefreshToken(user);

                var userTokens = new UserToken
                {
                    AccessToken = accessToken,
                    //RefreshToken = refreshToken,
                    ExpiredDateAccessToken = expiredAccess,
                    //ExpiredDateRefreshToken = expiredRefresh,
                    UserId = user.Id
                };
               // };
                await _userTokenService.SaveToken(userTokens);

                var response = new List <AuthenticationDTOResponse>
                {
                    new AuthenticationDTOResponse
                    {
                        AccessToken = userTokens.AccessToken,
                        AccessTokenExpiration = userTokens.ExpiredDateAccessToken,
                       // RefreshToken = userTokens.RefreshToken,
                       // RefreshTokenExpiration = userTokens.ExpiredDateRefreshToken,
                        UserName = user.UserName,
                        UserId = user.Id
                    }
                    
                };

                return response;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during login
                Console.WriteLine(ex);
                throw; // You might want to handle this differently based on your requirements
            }
        }
    }
}
