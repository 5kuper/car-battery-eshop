using BattAPI.Domain.Entities.Files;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Specific.Products
{
    public interface IGeneralProductService
    {
        Task<IList<GeneralProductDto>> ListAllKindsAsync();

        Task<ProductImageMeta?> GetImageMetaAsync(Guid productId);
        Task<ProductImageMeta> UpdateImageAsync(Guid productId, IFormFile image);
        Task DeleteImageAsync(Guid productId);
    }
}
