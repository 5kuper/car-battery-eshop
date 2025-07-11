using BattAPI.App.Specific.Products;
using BattAPI.App.Specific.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IList<GeneralProductDto>>> GetAll()
        {
            var result = await service.ListAsync();
            return Ok(result);
        }
    }
}
