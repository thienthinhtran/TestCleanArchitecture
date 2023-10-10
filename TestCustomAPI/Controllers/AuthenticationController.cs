using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using Service.Command;
using Service.Common;
using Service.Responses;
using Service.Validators;
using FluentValidation;

namespace YourApp.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO registrationModel)
        {
            if (registrationModel == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var command = new RegisterUserCommand
                {
                    UserName = registrationModel.UserName,
                    Password = registrationModel.Password,
                    Role = "User"
                };

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during user registration
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginModel)
        {
            
            if (loginModel == null)
            {
                //return BadRequest(ModelState);
                return BadRequest("Please fill in the space");
            }

            var command = new LoginUserCommand
            {
                UserName = loginModel.UserName,
                Password = loginModel.Password
            };
            LoginValidator validation = new();
            var check = await validation.ValidateAsync(command);
            if(!check.IsValid)
            {
                foreach (var failure in check.Errors)
                {
                    return BadRequest(failure.ErrorMessage);
                }
            }

            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
