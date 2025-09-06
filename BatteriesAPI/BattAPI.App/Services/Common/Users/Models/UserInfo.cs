namespace BattAPI.App.Services.Common.Users.Models
{
    public class UserInfo
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required bool IsAdmin { get; set; }
    }
}
