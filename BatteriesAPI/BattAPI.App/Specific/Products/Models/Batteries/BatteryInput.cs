using BattAPI.App.Specific.Products.Models.JustProducts;
using BattAPI.Domain.ValueObj;

namespace BattAPI.App.Specific.Products.Models.Batteries
{
    public class BatteryInput : JustProductInput
    {
        public string? Model { get; set; }

        public required BatterySpecs Specs { get; set; }

        public required IList<string> Tags { get; set; }
    }
}
