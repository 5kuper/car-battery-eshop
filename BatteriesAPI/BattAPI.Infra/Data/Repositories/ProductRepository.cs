using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories
{
    public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
    {
        protected override Expression<Func<Product, object?>>[] Includes => [b => b.ImageMeta];

        public async Task<IList<Product>> ListOfTypeAsync<T>() where T : Product
        {
            return await IncludingQuery
                .Where(p => EF.Property<string>(p, AppDbContext.DiscriminatorProperty) == typeof(T).Name)
                .ToListAsync();
        }
    }
}
