using BattAPI.Domain.Entities.Products;

namespace BattAPI.Domain.Entities.Files
{
    public class ProductImageMeta : FileMeta
    {
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
