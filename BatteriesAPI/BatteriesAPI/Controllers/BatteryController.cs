using BattAPI.App.Services.Abstractions;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    [Route("api/[controller]")]
    public class BatteryController(IBatteryRepository repo, IBatteryService service) : CrudControllerBase<Battery>(repo)
    {
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            var imageMeta = await service.GetImageMetaAsync(id);
            if (imageMeta != null)
            {
                return Ok($"{Request.Scheme}://{Request.Host}/{imageMeta.RelativePath}");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/image")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateImage(Guid id, IFormFile image)
        {
            var imageMeta = await service.UpdateImageAsync(id, image);
            return Ok($"{Request.Scheme}://{Request.Host}/{imageMeta.RelativePath}");
        }

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
