using Microsoft.AspNetCore.Mvc;
using TestCustomAPI.ViewModel;

namespace TestCustomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] AccountModel accountModel)
        {
            if (accountModel == null)
            {
                return BadRequest();
            }
            return Ok(true);
        }

    }
}
