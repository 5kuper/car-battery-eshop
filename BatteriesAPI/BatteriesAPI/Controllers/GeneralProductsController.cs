using BattAPI.App.Specific.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    [ApiController, Route("api/products")]
    public class GeneralProductsController(IGeneralProductService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IList<GeneralProductDto>>> GetAll()
        {
            var result = await service.ListAllKindsAsync();
            return Ok(result);
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            var imageMeta = await service.GetImageMetaAsync(id);
            if (imageMeta != null)
            {
                return Ok(new { url = $"{Request.Scheme}://{Request.Host}/{imageMeta.RelativePath}" });
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
            return Ok(new { url = $"{Request.Scheme}://{Request.Host}/{imageMeta.RelativePath}" });
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
