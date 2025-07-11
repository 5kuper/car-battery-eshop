using BattAPI.App.Specific.Products.Models.Batteries;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Specific.Products
{
    public interface IBatteryService : IDtoServiceBase<BatteryInput, BatteryDto, BatteryPatch>
    {
        Task<FileMeta?> GetImageMetaAsync(Guid batteryId);
        Task<FileMeta> UpdateImageAsync(Guid batteryId, IFormFile image);
        Task DeleteImageAsync(Guid batteryId);
    }
}
