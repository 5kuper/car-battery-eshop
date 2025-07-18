using BattAPI.Domain.Entities.Products;

namespace BattAPI.App.Specific.Products.JustProducts.Models
{
    public class JustProductPatch
    {
        public string? Name { get; set; }

        public string? Country { get; set; }


        public bool? InStock { get; set; }

        public int? Price { get; set; }

        public int? WarrantyMonths { get; set; }


        public void Patch(Product battery)
        {
            if (Name != null)
                battery.Name = Name;

            if (Country != null)
                battery.Country = Country;

            if (InStock.HasValue)
                battery.InStock = InStock.Value;

            if (Price.HasValue)
                battery.Price = Price.Value;

            if (WarrantyMonths.HasValue)
                battery.WarrantyMonths = WarrantyMonths.Value;
        }
    }
}
