using BattAPI.Domain.Entities;
using System.Linq.Expressions;

namespace BattAPI.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();

        Task<T?> GetAsync(Guid id);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, int? skip = null, int? take = null);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

        async Task<bool> ExistsAsync(Guid id)
        {
            return await GetAsync(id) != null;
        }

        async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAsync(predicate) != null;
        }
    }
}
