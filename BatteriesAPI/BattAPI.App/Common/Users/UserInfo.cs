namespace BattAPI.App.Common.Users
{
    public class UserInfo
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required bool IsAdmin { get; set; }
    }
}
