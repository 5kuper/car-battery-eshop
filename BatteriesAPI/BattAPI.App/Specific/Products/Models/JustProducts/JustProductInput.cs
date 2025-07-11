namespace BattAPI.App.Specific.Products.Models.JustProducts
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
