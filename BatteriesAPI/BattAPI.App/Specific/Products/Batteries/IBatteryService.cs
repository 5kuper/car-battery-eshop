using BattAPI.App.Specific.Products.Batteries.Models;
using BattAPI.App.Utils;

namespace BattAPI.App.Specific.Products.Batteries
{
    public interface IBatteryService : IDtoServiceBase<BatteryInput, BatteryDto, BatteryPatch>
    {

    }
}
