using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PMS.Core.Domain.Interfaces;

namespace PMS.Core.Infrastructure.RepoIMP
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PMSContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(PMSContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<IReadOnlyList<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }
        public async Task<T?> GetByIdWithIncludesAsync(Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public void Update(T entity)
            => _dbSet.Update(entity);

        public void Delete(T entity)
            => _dbSet.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
