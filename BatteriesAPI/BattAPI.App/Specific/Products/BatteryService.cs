using AutoMapper;
using BattAPI.App.Common.Files;
using BattAPI.App.Specific.Products.Models.Batteries;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Specific.Products
{
    public class BatteryService : DtoServiceBase<Battery, IBatteryRepository, BatteryInput, BatteryDto, BatteryPatch>, IBatteryService
    {
        private const string ImageUploadFolder = "batteries";

        private readonly IFileService<ProductImageMeta> _fileService;
        private readonly IHttpContextAccessor _ctxAccessor;

        public BatteryService(IBatteryRepository repo, IMapper mapper,
            IFileService<ProductImageMeta> fileService, IHttpContextAccessor ctxAccessor) : base(repo, mapper)
        {
            _fileService = fileService;
            _ctxAccessor = ctxAccessor;
        }

        public async Task<ProductImageMeta?> GetImageMetaAsync(Guid batteryId)
        {
            var battery = await Repository.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            return battery.ImageMeta;
        }

        public async Task<ProductImageMeta> UpdateImageAsync(Guid batteryId, IFormFile image)
        {
            var battery = await Repository.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            if (battery.ImageMeta != null)
                await _fileService.DeleteFileAsync(battery.ImageMeta.Id);

            return await _fileService.SaveImageFileAsync(image, ImageUploadFolder, meta => meta.ProductId = batteryId);
        }

        public async Task DeleteImageAsync(Guid batteryId)
        {
            var battery = await Repository.GetAsync(batteryId)
                ?? throw new ArgumentException("Battery not found.", nameof(batteryId));

            if (battery.ImageMeta != null)
                await _fileService.DeleteFileAsync(battery.ImageMeta.Id);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await DeleteImageAsync(id);
            await base.DeleteAsync(id);
        }

        protected override async Task<BatteryDto> MapToOutputAsync(Battery entity)
        {
            var result = await base.MapToOutputAsync(entity);

            if (entity.ImageMeta != null && _ctxAccessor.HttpContext != null)
            {
                var request = _ctxAccessor.HttpContext.Request;
                result.ImageUrl = $"{request.Scheme}://{request.Host}/{entity.ImageMeta.RelativePath}";
            }

            return result;
        }

        protected override void Patch(Battery entity, BatteryPatch patch) => patch.Patch(entity);
    }
}
