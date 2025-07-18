using AutoMapper;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.Repositories;
using BattAPI.App.Common.Files;
using BattAPI.Domain.Entities.Files;
using Microsoft.AspNetCore.Http;

using JProductInput = BattAPI.App.Specific.Products.JustProducts.Models.JustProductInput;
using JProductDto = BattAPI.App.Specific.Products.JustProducts.Models.JustProductDto;
using JProductPatch = BattAPI.App.Specific.Products.JustProducts.Models.JustProductPatch;

namespace BattAPI.App.Specific.Products.JustProducts
{
    public class JustProductService
        : DtoServiceBase<Product, IProductRepository, JProductInput, JProductDto, JProductPatch>, IJustProductService
    {
        private readonly IFileService<ProductImageMeta> _fileService;
        private readonly IHttpContextAccessor _ctxAccessor;

        public JustProductService(IProductRepository repo, IMapper mapper,
            IFileService<ProductImageMeta> fileService, IHttpContextAccessor ctxAccessor) : base(repo, mapper)
        {
            _fileService = fileService;
            _ctxAccessor = ctxAccessor;
        }

        public async Task<IList<JProductDto>> ListOnlyBaseProductsAsync()
        {
            var entities = await Repository.ListOfTypeAsync<Product>();

            var result = new List<JProductDto>();
            foreach (var entity in entities)
            {
                result.Add(await MapToOutputAsync(entity));
            }

            return result;
        }

        public string? GetImageUrl(Product product)
        {
            if (product.ImageMeta != null && _ctxAccessor.HttpContext != null)
            {
                var request = _ctxAccessor.HttpContext.Request;
                return $"{request.Scheme}://{request.Host}/{product.ImageMeta.RelativePath}";
            }

            return null;
        }

        public override async Task DeleteAsync(Guid id)
        {
            var product = await Repository.GetAsync(id);

            if (product != null)
            {
                if (product.ImageMeta != null)
                    await _fileService.DeleteFileAsync(product.ImageMeta.Id);

                await base.DeleteAsync(id);
            }
        }

        protected override async Task<JProductDto> MapToOutputAsync(Product entity)
        {
            var result = await base.MapToOutputAsync(entity);
            result.ImageUrl = GetImageUrl(entity);
            return result;
        }

        protected override void Patch(Product entity, JProductPatch patch) => patch.Patch(entity);
    }
}
