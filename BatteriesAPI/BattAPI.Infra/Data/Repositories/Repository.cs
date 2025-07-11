using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories
{
    public class Repository<T>(DbContext context) : IRepository<T> where T : Entity
    {
        protected DbContext Context { get; } = context;

        protected DbSet<T> Set { get; } = context.Set<T>();

        protected virtual Expression<Func<T, object?>>[] Includes { get; } = [];

        protected IQueryable<T> IncludingQuery
        {
            get
            {
                IQueryable<T> query = Set;
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
                return query;
            }
        }

        public async Task AddAsync(T entity)
        {
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
            await Set.AddAsync(entity);
        }

        public void Update(T entity) => Set.Update(entity);

        public void Remove(T entity) => Set.Remove(entity);

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();

        public async Task<T?> GetAsync(Guid id)
        {
            return await IncludingQuery.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await IncludingQuery.FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, int? skip = null, int? take = null)
        {
            var query = predicate != null
                ? IncludingQuery.Where(predicate)
                : IncludingQuery;

            query = query.OrderBy(e => e.Id);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate != null ? await Set.CountAsync(predicate) : await Set.CountAsync();
        }
    }
}
