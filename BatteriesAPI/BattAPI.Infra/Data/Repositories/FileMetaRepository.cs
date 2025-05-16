using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;

namespace BattAPI.Infra.Data.Repositories
{
    public class FileMetaRepository(AppDbContext context) : RepositoryBase<FileMeta>(context), IFileMetaRepository
    {
    }
}
