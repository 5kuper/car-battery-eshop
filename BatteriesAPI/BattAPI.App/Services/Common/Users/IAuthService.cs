using BattAPI.App.Services.Common.Users.Models;

namespace BattAPI.App.Services.Common.Users
{
    public record AuthOptions(string Secret, string AdminPassword);

    public interface IAuthService
    {
        Task<UserInfo?> GetUserInfoAsync(Guid userId);

        Task<string?> TryRegisterAsync(RegistrationData creds, string role = "user");

        Task<string?> TryLoginAsync(UserCreds creds);

        Task EnsureAdminRegisteredAsync();
    }
}
