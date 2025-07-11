using BattAPI.App.Common.Users;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BatteriesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await userRepo.GetAsync(u => u.Name == username);

            if (user == null || user.PasswordHash != Hash(password))
                return Unauthorized();

            var token = authService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (await userRepo.ExistsAsync(u => u.Name == username))
                return BadRequest("User already exists");

            var user = new User
            {
                Name = username,
                PasswordHash = Hash(password),
                Role = "user"
            };

            await userRepo.AddAsync(user);
            await userRepo.SaveChangesAsync();

            var token = authService.GenerateToken(user);
            return Ok(new { token });
        }

        private static string Hash(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
