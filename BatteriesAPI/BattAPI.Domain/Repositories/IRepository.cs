using BattAPI.Domain.Entities;
using System.Linq.Expressions;

namespace BattAPI.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null);

        Task<T?> GetAsync(Guid id);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        void Update(T entity);
        void Remove(T entity);

        Task SaveChangesAsync();

        async Task<bool> ExistsAsync(Guid id) => await GetAsync(id) != null;

        async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await ListAsync(predicate);
            return entities.Count != 0;
        }
    }
}
