using AutoMapper;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BattAPI.App.Common.Users
{
    public class AuthService(AuthOptions opt, IUserRepository userRepo, IMapper mapper) : IAuthService
    {
        public async Task<UserInfo> GetUserInfoAsync(Guid userId)
        {
            var user = await userRepo.GetAsync(userId);
            return mapper.Map<UserInfo>(user);
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
