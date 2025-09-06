using BattAPI.App.Services.Common.Files;
using BattAPI.App.Services.Specific.Products.Batteries;
using BattAPI.App.Services.Specific.Products.Batteries.Models;
using BattAPI.App.Services.Specific.Products.JustProducts;
using BattAPI.App.Services.Specific.Products.JustProducts.Models;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Services.Specific.Products
{
    public class GeneralProductService(IJustProductService jProductService, IBatteryService battService,
        IProductRepository repo, IFileService<ProductImageMeta> fileService) : IGeneralProductService
    {
        private const string ImageUploadFolder = "products";

        public async Task<IList<GeneralProductDto>> ListAllKindsAsync()
        {
            var justProducts = await jProductService.ListOnlyBaseProductsAsync();
            var resultJustProducts = justProducts.Select(ConvertJustProductToGeneralDto);

            var batteries = await battService.ListAsync();
            var resultBatteries = batteries.Select(ConvertBatteryToGeneralDto);

            var result = resultJustProducts.Concat(resultBatteries);
            return result.OrderBy(x => x.Title).ToList();
        }

        public async Task<ProductImageMeta?> GetImageMetaAsync(Guid productId)
        {
            var product = await repo.GetAsync(productId)
                ?? throw new ArgumentException("Product not found.", nameof(productId));

            return product.ImageMeta;
        }

        public async Task<ProductImageMeta> UpdateImageAsync(Guid productId, IFormFile image)
        {
            var product = await repo.GetAsync(productId)
                ?? throw new ArgumentException("Product not found.", nameof(productId));

            if (product.ImageMeta != null)
                await fileService.DeleteFileAsync(product.ImageMeta.Id);

            return await fileService.SaveImageFileAsync(image, ImageUploadFolder, meta => meta.ProductId = productId);
        }

        public async Task DeleteImageAsync(Guid productId)
        {
            var product = await repo.GetAsync(productId)
                ?? throw new ArgumentException("Product not found.", nameof(productId));

            if (product.ImageMeta != null)
                await fileService.DeleteFileAsync(product.ImageMeta.Id);
        }

        private static GeneralProductDto ConvertJustProductToGeneralDto(JustProductDto product)
        {
            return new()
            {
                Id = product.Id,
                Title = product.Name,
                InStock = product.InStock,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Labels =
                [
                    $"{product.WarrantyMonths} месяцев гарантии"
                ]
            };
        }

        private static GeneralProductDto ConvertBatteryToGeneralDto(BatteryDto batt)
        {
            var result = ConvertJustProductToGeneralDto(batt);

            result.Title = batt.Name + (batt.Model != null ? $" {batt.Model}" : "") + $" {batt.Specs.Capacity}";

            result.Labels.Add($"{batt.Specs.Voltage}V");
            result.Labels.Add($"{batt.Specs.StartPower} {batt.Specs.StartPowerRating}");

            foreach (var tag in batt.Tags)
            {
                result.Labels.Add(tag);
            }

            return result;
        }
    }
}
