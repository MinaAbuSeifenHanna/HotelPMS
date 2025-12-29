using System.Linq.Expressions;

namespace PMS.Core.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>>? predicate = null,
                    params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdWithIncludesAsync(Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task SaveChangesAsync();
    }
}
