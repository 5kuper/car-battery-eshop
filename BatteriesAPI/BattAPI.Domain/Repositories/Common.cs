using BattAPI.Domain.Entities;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Entities.Products;

namespace BattAPI.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>;

    public interface IProductRepository : IRepository<Product>;
    public interface IBatteryRepository : IRepository<Battery>;

    public interface IFileMetaRepository : IRepository<FileMeta>;
    public interface IProductImageRepository : IRepository<ProductImageMeta>;
}
