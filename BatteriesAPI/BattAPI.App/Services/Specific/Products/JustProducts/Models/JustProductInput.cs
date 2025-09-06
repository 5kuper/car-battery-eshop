namespace BattAPI.App.Services.Specific.Products.JustProducts.Models
{
    public class JustProductInput
    {
        public required string Name { get; set; }

        public string? Country { get; set; }


        public bool InStock { get; set; }

        public int Price { get; set; }

        public int WarrantyMonths { get; set; }
    }
}
