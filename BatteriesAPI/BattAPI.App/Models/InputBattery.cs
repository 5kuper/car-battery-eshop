using BattAPI.Domain.Entities;
using BattAPI.Domain.ValueObj;

namespace BattAPI.App.Models
{
    public class InputBattery
    {
        public string? Name { get; set; }

        public int? Capacity { get; set; }

        public int? Voltage { get; set; }

        public int? StartPower { get; set; }

        public StartPowerRating? StartPowerRating { get; set; }

        public int? Price { get; set; }

        public void Patch(Battery battery)
        {
            if (Name != null)
                battery.Name = Name;

            if (Capacity != null)
                battery.Capacity = Capacity.Value;

            if (Voltage != null)
                battery.Voltage = Voltage.Value;

            if (StartPower != null)
                battery.StartPower = StartPower.Value;

            if (StartPowerRating != null)
                battery.StartPowerRating = StartPowerRating.Value;

            if (Price != null)
                battery.Price = Price.Value;
        }
    }
}
