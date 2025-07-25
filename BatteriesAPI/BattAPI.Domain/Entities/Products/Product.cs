﻿using BattAPI.Domain.Entities.Files;

namespace BattAPI.Domain.Entities.Products
{
    public class Product : Entity
    {
        public required string Name { get; set; }

        public string? Country { get; set; }


        public bool InStock { get; set; }

        public int Price { get; set; }

        public int WarrantyMonths { get; set; }


        public ProductImageMeta? ImageMeta { get; set; }
    }
}
