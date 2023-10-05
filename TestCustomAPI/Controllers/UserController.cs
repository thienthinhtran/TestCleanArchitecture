using Data.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common;
using Service;
using Domain.Entities;

namespace TestCustomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDapperHelper _dapperHelper;

        public UserController(IUserService userService, IDapperHelper dapperHelper)
        {
            _userService = userService;
            _dapperHelper = dapperHelper;
        }

        [HttpGet("all")]
        [Authorize(Policy = "AdminOnly")] // Apply authorization policy
        public async Task<IActionResult> GetAllUsersAsync()
        {
           // return Ok("Admin-only endpoint accessed successfully.");
            try
            {
                // Replace the following line with your Dapper-based code to fetch all users
                var users = await _dapperHelper.ExecuteSqlReturnList<User>("SELECT Id, UserName, Password, DisplayName, LastLoggedIn, CreatedDate, Role FROM [User]");

                return Ok(users);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during data retrieval
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
