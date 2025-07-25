using BattAPI.App.Common.Users;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BatteriesAPI.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthController(IAuthService authService, IUserRepository userRepo)
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
        public async Task<IActionResult> Register(string username, string password)
        {
            var token = authService.TryRegisterAsync(username, password);

            if (token == null)
                return BadRequest("User already exists");

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var token = await authService.TryLoginAsync(username, password);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
