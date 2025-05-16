using BattAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Services.Abstractions
{
    public interface IBatteryService
    {
        Task<FileMeta?> GetImageMetaAsync(Guid batteryId);

        Task<FileMeta> UpdateImageAsync(Guid batteryId, IFormFile image);
    }
}
