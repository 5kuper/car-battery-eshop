using AutoMapper;
using BattAPI.App.Specific.Products.Batteries.Models;
using BattAPI.App.Specific.Products.JustProducts;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.Repositories;

namespace BattAPI.App.Specific.Products.Batteries
{
    public class BatteryService(IBatteryRepository repo, IMapper mapper, IJustProductService jProductService)
        : DtoServiceBase<Battery, IBatteryRepository, BatteryInput, BatteryDto, BatteryPatch>(repo, mapper), IBatteryService
    {
        private readonly IJustProductService _jProductService = jProductService;

        public override async Task DeleteAsync(Guid id) => await _jProductService.DeleteProductOfAnyKindAsync(id);

        protected override async Task<BatteryDto> MapToOutputAsync(Battery entity)
        {
            var result = await base.MapToOutputAsync(entity);
            result.ImageUrl = _jProductService.GetImageUrl(entity);
            return result;
        }

        protected override void Patch(Battery entity, BatteryPatch patch) => patch.Patch(entity);
    }
}
