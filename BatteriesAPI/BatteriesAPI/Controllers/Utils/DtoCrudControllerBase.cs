using BattAPI.App.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers.Utils
{
    public class DtoCrudControllerBase<TService, TInput, TOutput, TPatch>(TService dtoService) : ControllerBase
        where TService : IDtoServiceBase<TInput, TOutput, TPatch>
    {
        protected TService Service { get; } = dtoService;

        [HttpGet]
        public virtual async Task<ActionResult<IList<TOutput>>> GetAll()
        {
            var result = await Service.ListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TOutput>> GetById(Guid id)
        {
            var result = await Service.GetAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TInput input)
        {
            var id = await Service.CreateAsync(input);
            return Ok(new { id });
        }

        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, TPatch patch)
        {
            await Service.UpdateAsync(id, patch);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await Service.DeleteAsync(id);
            return Ok();
        }
    }
}
