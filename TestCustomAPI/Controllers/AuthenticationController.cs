using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using ServiceAuthentication;
using TestCustomAPI.ViewModel;

namespace TestCustomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenSerive;

        public AuthenticationController(IUserService userService, ITokenHandler tokenHandler, IUserTokenService userTokenSerive)
        {
            _tokenHandler = tokenHandler;
            _userService = userService;
            _userTokenSerive = userTokenSerive;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] AccountModel accountModel)
        {
            /*var validations = await validator.ValidateAsync(accountModel);

            if (!validations.IsValid)
            {
                return BadRequest(validations.Errors);
            }*/
            if(accountModel == null)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.CheckLogin(accountModel.Username, accountModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            (string accessToken, DateTime expiredAccess)  = await _tokenHandler.CreateAccessToken(user);
            (string refreshToken, DateTime expiredRefresh)  = await _tokenHandler.CreateRefreshToken(user);
            try
            {
                await _userTokenSerive.SaveToken(new List<UserToken>
                {
                    new UserToken
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        ExpiredDateAccessToken = expiredAccess,
                        ExpiredDateRefreshToken = expiredRefresh,
                        UserId = user.Id
                    }
                });

                return Ok(new JwtModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    FullName = user.DisplayName,
                    UserName = user.UserName
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during token save
                return StatusCode(500, "Internal server error");
            }
            
        }

    }
}
