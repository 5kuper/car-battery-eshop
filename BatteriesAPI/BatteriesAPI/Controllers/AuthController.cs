using BattAPI.App.Services.Common.Users;
using BattAPI.App.Services.Common.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BatteriesAPI.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthController(IAuthService authService)
        : ControllerBase
    {
        [HttpGet("user-info"), Authorize]
        public async Task<ActionResult<UserInfo>> GetUserInfo()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null) return Forbid();

            var id = Guid.Parse(idClaim.Value);
            var result = await authService.GetUserInfoAsync(id);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationData creds)
        {
            var token = await authService.TryRegisterAsync(creds);

            if (token == null)
                return Conflict("User already exists");

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCreds creds)
        {
            var token = await authService.TryLoginAsync(creds);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
