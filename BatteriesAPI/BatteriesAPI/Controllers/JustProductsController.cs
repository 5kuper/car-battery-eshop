using BatteriesAPI.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BattAPI.App.Services.Specific.Products.JustProducts;
using BattAPI.App.Services.Specific.Products.JustProducts.Models;

namespace BatteriesAPI.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class JustProductsController(IJustProductService service)
        : DtoCrudControllerBase<IJustProductService, JustProductInput, JustProductDto, JustProductPatch>(service)
    {
        [NonAction]
        public override Task<ActionResult<IList<JustProductDto>>> GetAll() => base.GetAll();

        [HttpGet]
        public virtual async Task<ActionResult<IList<JustProductDto>>> GetAll(bool onlyBase = false)
        {
            var result = onlyBase ? await Service.ListOnlyBaseProductsAsync() : await Service.ListAsync();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Create(JustProductInput input)
        {
            return base.Create(input);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Update(Guid id, JustProductPatch patch)
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
