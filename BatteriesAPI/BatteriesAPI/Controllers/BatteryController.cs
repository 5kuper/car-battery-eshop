using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    [Route("api/[controller]")]
    public class BatteryController(IBatteryRepository repo) : CrudControllerBase<Battery>(repo)
    {
        [Authorize(Roles = "admin")]
        public override Task<ActionResult<Battery>> Create(Battery entity)
        {
            return base.Create(entity);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Update(Guid id, Battery entity)
        {
            return base.Update(id, entity);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Remove(Guid id)
        {
            return base.Remove(id);
        }
    }
}
