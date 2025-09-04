namespace BattAPI.App.Common.Users
{
    public record AuthOptions(string Secret, string AdminPassword);

    public interface IAuthService
    {
        Task<UserInfo?> GetUserInfoAsync(Guid userId);

        Task<string?> TryRegisterAsync(UserCreds creds, string role = "user");

        Task<string?> TryLoginAsync(UserCreds creds);

        Task EnsureAdminRegisteredAsync();
    }
}
