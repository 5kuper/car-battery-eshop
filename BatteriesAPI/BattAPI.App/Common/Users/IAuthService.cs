namespace BattAPI.App.Common.Users
{
    public record AuthOptions(string Secret, string AdminPassword);

    public interface IAuthService
    {
        Task<UserInfo> GetUserInfoAsync(Guid userId);

        Task<string?> TryRegisterAsync(string username, string password, string role = "user");

        Task<string?> TryLoginAsync(string username, string password);

        Task EnsureAdminRegisteredAsync();
    }
}
