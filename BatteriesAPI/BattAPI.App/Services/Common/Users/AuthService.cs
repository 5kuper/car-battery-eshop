using AutoMapper;
using BattAPI.App.Services.Common.Users.Models;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BattAPI.App.Services.Common.Users
{
    public class AuthService(AuthOptions opt, IUserRepository userRepo, IMapper mapper) : IAuthService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public async Task<UserInfo?> GetUserInfoAsync(Guid userId)
        {
            var user = await userRepo.GetAsync(userId);
            return mapper.Map<UserInfo>(user);
        }

        public async Task EnsureAdminRegisteredAsync()
        {
            var isAdminExist = await userRepo.ExistsAsync(u => u.Name == "admin");
            if (!isAdminExist)
                await TryRegisterAsync(new("admin", opt.AdminPassword), "admin");
        }

        public async Task<string?> TryRegisterAsync(RegistrationData creds, string role = "user")
        {
            var username = creds.Username.Trim().ToLowerInvariant();

            if (await userRepo.ExistsAsync(u => u.Name == username))
                return null;

            var user = new User { Name = username, Role = role };
            user.PasswordHash = _hasher.HashPassword(user, creds.Password);

            await userRepo.AddAsync(user);
            await userRepo.SaveChangesAsync();

            return GenerateToken(user);
        }

        public async Task<string?> TryLoginAsync(UserCreds creds)
        {
            var username = creds.Username.Trim().ToLowerInvariant();

            var user = await userRepo.GetAsync(u => u.Name == username);
            if (user is null) return null;

            var vr = _hasher.VerifyHashedPassword(user, user.PasswordHash, creds.Password);

            if (vr == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _hasher.HashPassword(user, creds.Password);
                await userRepo.SaveChangesAsync();
            }
            else if (vr == PasswordVerificationResult.Failed)
            {
                return null;
            }

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
    }
}
