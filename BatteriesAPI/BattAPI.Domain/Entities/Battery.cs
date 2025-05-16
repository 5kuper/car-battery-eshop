using BattAPI.Domain.ValueObj;

namespace BattAPI.Domain.Entities
{
    public class Battery : EntityBase
    {
        public required string Name { get; set; }

        public required int Capacity {  get; set; }

        public required int Voltage { get; set; }

        public required int StartPower { get; set; }

        public required StartPowerRating StartPowerRating { get; set; }

        public int Price { get; set; }

        public Guid? ImageMetaId { get; set; }
    }
}
