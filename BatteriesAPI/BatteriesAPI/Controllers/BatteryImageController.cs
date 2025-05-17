using BattAPI.App.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    [Route("api/Battery")]
    [ApiController]
    public class BatteryImageController(IBatteryService service) : ControllerBase
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

        [HttpDelete("{id}/image")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            await service.DeleteImageAsync(id);
            return Ok();
        }
    }
}
