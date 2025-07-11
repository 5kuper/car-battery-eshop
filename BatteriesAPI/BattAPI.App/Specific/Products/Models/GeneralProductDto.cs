namespace BattAPI.App.Specific.Products.Models
{
    public class GeneralProductDto
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required IList<string> Labels { get; set; }

        public bool InStock { get; set; }

        public int Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
