using Dapper;
using Data.Abstraction;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Common;
using ServiceAuthentication;
using System.Data;
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
        private readonly IDapperHelper _dapperHelper;

        public AuthenticationController(IUserService userService, ITokenHandler tokenHandler, IUserTokenService userTokenSerive, IDapperHelper dapperHelper)
        {
            _tokenHandler = tokenHandler;
            _userService = userService;
            _userTokenSerive = userTokenSerive;
            _dapperHelper = dapperHelper;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDTO registrationModel)
        {
            if (registrationModel == null)
            {
                return BadRequest(ModelState);
            }

            // Check if the username is already taken
            var existingUser = await _userService.FindByUsername(registrationModel.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return BadRequest(ModelState);
            }

            // Create a new user entity and set properties
            var newUser = new User
            {
                UserName = registrationModel.UserName,
                Password = registrationModel.Password,
                Role = "User"
            };

            // Add the new user to the database using Dapper
            try
            {
                // Create a DynamicParameters object and add parameters
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", newUser.UserName);
                parameters.Add("@Password", newUser.Password);
                parameters.Add("@Role", newUser.Role);

                // Replace the following line with your Dapper-based code for inserting the user
                await _dapperHelper.ExecuteNotReturnAsync("INSERT INTO [User] (UserName, Password, Role) VALUES (@UserName, @Password, @Role)", parameters);

                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during user insertion
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] AccountModel accountModel)
        {
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
