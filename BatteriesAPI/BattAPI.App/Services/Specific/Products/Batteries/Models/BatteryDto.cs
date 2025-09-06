using BattAPI.App.Services.Specific.Products.JustProducts.Models;
using BattAPI.Domain.ValueObj;

namespace BattAPI.App.Services.Specific.Products.Batteries.Models
{
    public class BatteryDto : JustProductDto
    {
        public string? Model { get; set; }

        public required BatterySpecs Specs { get; set; }

        public required IList<string> Tags { get; set; }
    }
}
