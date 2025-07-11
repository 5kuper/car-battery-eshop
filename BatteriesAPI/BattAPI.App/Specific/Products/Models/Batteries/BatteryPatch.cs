using BattAPI.App.Specific.Products.Models.JustProducts;
using BattAPI.Domain.Entities.Products;
using BattAPI.Domain.ValueObj;

namespace BattAPI.App.Specific.Products.Models.Batteries
{
    public class BatteryPatch : JustProductPatch
    {
        public string? Model { get; set; }

        public BatterySpecs? Specs { get; set; }

        public IList<string>? Tags { get; set; }

        public void Patch(Battery battery)
        {
            base.Patch(battery);

            if (Model != null)
                battery.Model = Model;

            if (Specs != null)
                battery.Specs = Specs;

            if (Tags != null)
                battery.Tags = Tags;
        }
    }
}
