namespace BattAPI.App.Specific.Products.JustProducts.Models
{
    public class JustProductDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }


        public required string Name { get; set; }

        public string? Country { get; set; }


        public bool InStock { get; set; }

        public int Price { get; set; }

        public int WarrantyMonths { get; set; }


        public string? ImageUrl { get; set; }
    }
}
