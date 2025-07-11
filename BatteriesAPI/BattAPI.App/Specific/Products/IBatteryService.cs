using BattAPI.App.Specific.Products.Models.Batteries;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities.Files;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Specific.Products
{
    public interface IBatteryService : IDtoServiceBase<BatteryInput, BatteryDto, BatteryPatch>
    {
        Task<ProductImageMeta?> GetImageMetaAsync(Guid batteryId);
        Task<ProductImageMeta> UpdateImageAsync(Guid batteryId, IFormFile image);
        Task DeleteImageAsync(Guid batteryId);
    }
}
