using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Repositories;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories.Files
{
    public class ProductImageRepository(AppDbContext context) : Repository<ProductImageMeta>(context), IProductImageRepository
    {
        protected override Expression<Func<ProductImageMeta, object?>>[] Includes => [img => img.Product];
    }
}
