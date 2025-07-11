using BattAPI.Domain.ValueObj;

namespace BattAPI.Domain.Entities.Products
{
    public class Battery : Product
    {
        public string? Model { get; set; }

        public required BatterySpecs Specs { get; set; }

        public required IList<string> Tags { get; set; }
    }
}
