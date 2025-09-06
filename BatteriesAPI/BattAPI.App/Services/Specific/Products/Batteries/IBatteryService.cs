using BattAPI.App.Services.Specific.Products.Batteries.Models;
using BattAPI.App.Utils;

namespace BattAPI.App.Services.Specific.Products.Batteries
{
    public interface IBatteryService : IDtoServiceBase<BatteryInput, BatteryDto, BatteryPatch>
    {

    }
}
