namespace BattAPI.Domain.Entities
{
    public class User : Entity
    {
        public required string Name { get; set; }

        public string PasswordHash { get; set; } = null!;

        public required string Role { get; set; }
    }
}
