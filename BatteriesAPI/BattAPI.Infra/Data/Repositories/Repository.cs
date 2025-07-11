using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _set;

        public Repository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<IList<T>> ListAsync(Expression<Func<T, bool>>? predicate)
        {
            if (predicate != null)
            {
                return await _set.Where(predicate).ToListAsync();
            }
            else
            {
                return await _set.ToListAsync();
            }
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }

        public void Remove(T entity)
        {
            _set.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
