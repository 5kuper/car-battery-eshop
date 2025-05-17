using BattAPI.App.Models;
using BattAPI.App.Services.Abstractions;
using BatteriesAPI.Controllers.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    [Route("api/[controller]")]
    public class BatteriesController(IBatteryService service)
        : DtoCrudControllerBase<IBatteryService, InputBattery, OutputBattery, InputBattery>(service)
    {
        [Authorize(Roles = "admin")]
        public override Task<ActionResult<OutputBattery>> Create(InputBattery input)
        {
            return base.Create(input);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Update(Guid id, InputBattery patch)
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
