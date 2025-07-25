using AutoMapper;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public async Task EnsureAdminRegisteredAsync()
        {
            var isAdminExist = await userRepo.ExistsAsync(u => u.Name == "admin");
            if (!isAdminExist)
                await TryRegisterAsync("admin", opt.AdminPassword, "admin");
        }

        public async Task<string?> TryRegisterAsync(string username, string password, string role = "user")
        {
            if (await userRepo.ExistsAsync(u => u.Name == username))
                return null;

            var user = new User
            {
                Name = username,
                PasswordHash = Hash(password),
                Role = role
            };

            await userRepo.AddAsync(user);
            await userRepo.SaveChangesAsync();

            return GenerateToken(user);
        }

        public async Task<string?> TryLoginAsync(string username, string password)
        {
            var user = await userRepo.GetAsync(u => u.Name == username);

            if (user == null || user.PasswordHash != Hash(password))
                return null;

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
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

        private static string Hash(string value)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
            return Convert.ToBase64String(bytes);
        }
    }
}
