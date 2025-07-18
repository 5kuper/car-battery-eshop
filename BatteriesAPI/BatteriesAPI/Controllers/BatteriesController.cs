using BattAPI.App.Specific.Products.Batteries;
using BattAPI.App.Specific.Products.Batteries.Models;
using BatteriesAPI.Controllers.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class BatteriesController(IBatteryService service)
        : DtoCrudControllerBase<IBatteryService, BatteryInput, BatteryDto, BatteryPatch>(service)
    {
        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Create(BatteryInput input)
        {
            return base.Create(input);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Update(Guid id, BatteryPatch patch)
        {
            return base.Update(id, patch);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}
