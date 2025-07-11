using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.Repositories;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories
{
    public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
    {
        protected override Expression<Func<Product, object?>>[] Includes => [b => b.ImageMeta];
    }
}
