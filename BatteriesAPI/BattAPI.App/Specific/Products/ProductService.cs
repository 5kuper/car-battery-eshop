using BattAPI.App.Specific.Products.Models;
using BattAPI.App.Specific.Products.Models.Batteries;

namespace BattAPI.App.Specific.Products
{
    public class ProductService(IBatteryService batteryService) : IProductService
    {
        public async Task<IList<GeneralProductDto>> ListAsync()
        {
            var batteries = await batteryService.ListAsync();
            return batteries.Select(ConvertBatteryToGeneralDto).ToList();
        }

        private GeneralProductDto ConvertBatteryToGeneralDto(BatteryDto batt)
        {
            return new()
            {
                Id = batt.Id,
                Title = batt.Name + (batt.Model != null ? $" {batt.Model}" : "") + $" {batt.Specs.Capacity}",
                InStock = batt.InStock,
                Price = batt.Price,
                Labels = new List<string>(batt.Tags)
                {
                    $"{batt.Specs.Voltage}V",
                    $"{batt.Specs.StartPower} {batt.Specs.StartPowerRating}",
                    $"{batt.WarrantyMonths} месяцев гарантии"
                },
                ImageUrl = batt.ImageUrl
            };
        }
    }
}
