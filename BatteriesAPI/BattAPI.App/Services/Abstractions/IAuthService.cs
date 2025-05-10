using BattAPI.Domain.Entities;

namespace BattAPI.App.Services.Abstractions
{
    public record AuthOptions(string Secret);

    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
