using BattAPI.Domain.Entities;

namespace BattAPI.App.Services.Abstractions
{
    public record AuthOptions(string Secret);

    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
