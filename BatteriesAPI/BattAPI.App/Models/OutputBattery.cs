using BattAPI.Domain.ValueObj;

namespace BattAPI.App.Models
{
    public class OutputBattery
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required int Capacity { get; set; }

        public required int Voltage { get; set; }

        public required int StartPower { get; set; }

        public required StartPowerRating StartPowerRating { get; set; }

        public int Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
