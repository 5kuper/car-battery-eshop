using AutoMapper;
using BattAPI.App.Services.Abstractions;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;

namespace BattAPI.App.Services.Implementations
{
    public abstract class DtoServiceBase<TEntity, TRepository, TInput, TOutput, TPatch>(TRepository repo, IMapper mapper)
        : IDtoServiceBase<TInput, TOutput, TPatch>

        where TEntity : EntityBase
        where TRepository : IRepository<TEntity>
        where TInput : class
    {
        protected TRepository Repository { get; } = repo;

        public async Task<IList<TOutput>> ListAsync()
        {
            var entities = await Repository.ListAsync();

            var result = new List<TOutput>();
            foreach (var entity in entities)
            {
                result.Add(await MapToOutputAsync(entity));
            }

            return result;
        }

        public async Task<TOutput?> GetAsync(Guid id)
        {
            var entity = await Repository.GetAsync(id);

            return entity != null
                ? await MapToOutputAsync(entity)
                : default;
        }

        public async Task<Guid> CreateAsync(TInput input)
        {
            var entity = mapper.Map<TEntity>(input);

            await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(Guid id, TPatch patch)
        {
            var entity = await Repository.GetAsync(id)
                ?? throw new ArgumentException("Entity not found.", nameof(id));

            Patch(entity, patch);

            Repository.Update(entity);
            await Repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await Repository.GetAsync(id)
                ?? throw new ArgumentException("Entity not found.", nameof(id));

            if (entity != null)
                Repository.Remove(entity);

            await Repository.SaveChangesAsync();
        }

        protected virtual Task<TOutput> MapToOutputAsync(TEntity entity)
        {
            var result = mapper.Map<TOutput>(entity);
            return Task.FromResult(result);
        }

        protected abstract void Patch(TEntity entity, TPatch patch);
    }
}
