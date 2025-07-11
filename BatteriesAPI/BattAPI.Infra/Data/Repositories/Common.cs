using BattAPI.Domain.Entities;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Repositories;

namespace BattAPI.Infra.Data.Repositories
{
    public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository;

    public class FileMetaRepository(AppDbContext context) : Repository<FileMeta>(context), IFileMetaRepository;
}
