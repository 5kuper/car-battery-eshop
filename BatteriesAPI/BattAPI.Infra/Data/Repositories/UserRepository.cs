using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;

namespace BattAPI.Infra.Data.Repositories
{
    public class UserRepository(AppDbContext context) : RepositoryBase<User>(context), IUserRepository
    {
    }
}
