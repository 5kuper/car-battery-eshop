using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.Repositories;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories
{
    public class BatteryRepository(AppDbContext context) : Repository<Battery>(context), IBatteryRepository
    {
        protected override Expression<Func<Battery, object?>>[] Includes => [b => b.ImageMeta];
    }
}
