namespace BattAPI.Domain.Entities
{
    public class User : Entity
    {
        public required string Name { get; set; }

        public required string PasswordHash { get; set; }

        public required string Role { get; set; }
    }
}
