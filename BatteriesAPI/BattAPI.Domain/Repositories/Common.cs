using BattAPI.Domain.Entities;
using BattAPI.Domain.Entities.Products;

namespace BattAPI.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>;

    public interface IBatteryRepository : IRepository<Battery>;

    public interface IFileMetaRepository : IRepository<FileMeta>;
}
