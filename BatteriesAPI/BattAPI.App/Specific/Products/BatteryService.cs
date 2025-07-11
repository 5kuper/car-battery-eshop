using AutoMapper;
using BattAPI.App.Common.Files;
using BattAPI.App.Specific.Products.Models.Batteries;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Specific.Products
{
    public class BatteryService(IBatteryRepository repo, IMapper mapper, IFileService fileService, IHttpContextAccessor ctxAccessor)
        : DtoServiceBase<Battery, IBatteryRepository, BatteryInput, BatteryDto, BatteryPatch>(repo, mapper), IBatteryService
    {
        private const string ImageUploadFolder = "batteries";

        private readonly IFileService _fileService = fileService;
        private readonly IHttpContextAccessor _ctxAccessor = ctxAccessor;

        public async Task<FileMeta?> GetImageMetaAsync(Guid batteryId)
        {
            var battery = await Repository.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            if (battery.ImageMetaId == null)
                return null;

            return await _fileService.GetFileMetaAsync(battery.ImageMetaId.Value);
        }

        public async Task<FileMeta> UpdateImageAsync(Guid batteryId, IFormFile image)
        {
            var battery = await Repository.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            var imageMeta = await _fileService.SaveImageFileAsync(image, ImageUploadFolder);

            if (battery.ImageMetaId != null)
                await _fileService.DeleteFileAsync(battery.ImageMetaId.Value);

            battery.ImageMetaId = imageMeta.Id;
            await Repository.SaveChangesAsync();

            return imageMeta;
        }

        public async Task DeleteImageAsync(Guid batteryId)
        {
            var battery = await Repository.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            if (battery.ImageMetaId == null)
                throw new InvalidOperationException("Battery has not image.");

            await _fileService.DeleteFileAsync(battery.ImageMetaId.Value);
            battery.ImageMetaId = null;
            await Repository.SaveChangesAsync();
        }

        public override async Task DeleteAsync(Guid id)
        {
            await DeleteImageAsync(id);
            await base.DeleteAsync(id);
        }

        protected override async Task<BatteryDto> MapToOutputAsync(Battery entity)
        {
            var result = await base.MapToOutputAsync(entity);

            if (entity.ImageMetaId != null)
            {
                var imageMeta = await _fileService.GetFileMetaAsync(entity.ImageMetaId.Value)
                    ?? throw new InvalidOperationException("Image meta not found.");

                if (_ctxAccessor.HttpContext != null)
                {
                    var request = _ctxAccessor.HttpContext.Request;
                    result.ImageUrl = $"{request.Scheme}://{request.Host}/{imageMeta.RelativePath}";
                }
            }

            return result;
        }

        protected override void Patch(Battery entity, BatteryPatch patch) => patch.Patch(entity);
    }
}
