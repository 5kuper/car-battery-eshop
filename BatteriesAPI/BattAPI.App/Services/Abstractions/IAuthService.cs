using BattAPI.App.Models;
using BattAPI.Domain.Entities;

namespace BattAPI.App.Services.Abstractions
{
    public record AuthOptions(string Secret);

    public interface IAuthService
    {
        Task<UserInfo> GetUserInfoAsync(Guid userId);

        string GenerateToken(User user);
    }
}
