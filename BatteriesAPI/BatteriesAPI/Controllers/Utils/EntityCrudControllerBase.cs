using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers.Utils
{
    public class EntityCrudControllerBase<T>(IRepository<T> repo) : ControllerBase where T : EntityBase
    {
        [HttpGet]
        public virtual async Task<ActionResult<IList<T>>> GetAll()
        {
            var entities = await repo.ListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(Guid id)
        {
            var entity = await repo.GetAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(T entity)
        {
            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            return Ok(new { id = entity.Id });
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, T entity)
        {
            if (id != entity.Id)
                return BadRequest();

            if (!await repo.ExistsAsync(id))
                return NotFound();

            repo.Update(entity);
            await repo.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var entity = await repo.GetAsync(id);

            if (entity == null)
                return NotFound();

            repo.Remove(entity);
            await repo.SaveChangesAsync();

            return Ok();
        }
    }
}
