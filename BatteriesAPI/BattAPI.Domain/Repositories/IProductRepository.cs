using BattAPI.Domain.Entities.Products;

namespace BattAPI.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IList<Product>> ListOfTypeAsync<T>() where T : Product;
    }
}
