using BattAPI.App.Services.Abstractions;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Services.Implementations
{
    public class BatteryService(IBatteryRepository batteryRepo, IFileService fileService) : IBatteryService
    {
        public async Task<FileMeta?> GetImageMetaAsync(Guid batteryId)
        {
            var battery = await batteryRepo.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            if (battery.ImageMetaId == null)
                return null;

            var imageMeta = await fileService.GetFileMetaAsync(battery.ImageMetaId.Value)
                ?? throw new InvalidOperationException("Image meta not found.");

            return imageMeta;
        }

        public async Task<FileMeta> UpdateImageAsync(Guid batteryId, IFormFile image)
        {
            var battery = await batteryRepo.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            var imageMeta = await fileService.SaveImageFileAsync(image, "batteries");

            if (battery.ImageMetaId != null)
                await fileService.DeleteFileAsync(battery.ImageMetaId.Value);

            battery.ImageMetaId = imageMeta.Id;
            await batteryRepo.SaveChangesAsync();

            return imageMeta;
        }
    }
}
