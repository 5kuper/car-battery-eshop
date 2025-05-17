using BattAPI.App.Models;
using BattAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Services.Abstractions
{
    public interface IBatteryService : IDtoServiceBase<InputBattery, OutputBattery, InputBattery>
    {
        Task<FileMeta?> GetImageMetaAsync(Guid batteryId);
        Task<FileMeta> UpdateImageAsync(Guid batteryId, IFormFile image);
        Task DeleteImageAsync(Guid batteryId);
    }
}
