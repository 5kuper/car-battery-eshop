using BattAPI.Domain.Entities;

namespace BattAPI.App.Common.Users
{
    public record AuthOptions(string Secret);

    public interface IAuthService
    {
        Task<UserInfo> GetUserInfoAsync(Guid userId);

        string GenerateToken(User user);
    }
}
