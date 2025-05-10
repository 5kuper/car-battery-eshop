using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BatteriesAPI.Controllers
{
    public class CrudControllerBase<T>(IRepository<T> repo) : ControllerBase where T : EntityBase
    {
        [HttpGet]
        public async virtual Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var entities = await repo.ListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async virtual Task<ActionResult<T>> GetById(Guid id)
        {
            var entity = await repo.GetAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async virtual Task<ActionResult<T>> Create(T entity)
        {
            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async virtual Task<IActionResult> Update(Guid id, T entity)
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
        public async virtual Task<IActionResult> Remove(Guid id)
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
