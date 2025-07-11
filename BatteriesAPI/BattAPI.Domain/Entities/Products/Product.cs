namespace BattAPI.Domain.Entities.Products
{
    public class Product : EntityBase
    {
        public required string Name { get; set; }

        public string? Country { get; set; }


        public bool InStock { get; set; }

        public int Price { get; set; }

        public int WarrantyMonths { get; set; }


        public Guid? ImageMetaId { get; set; }
    }
}
