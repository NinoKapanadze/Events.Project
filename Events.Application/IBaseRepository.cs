using System.Linq.Expressions;

namespace Events.Application
{
    public interface IBaseRepository<T> where T : class
    {
        Task SaveChanges(CancellationToken token);

        Task<List<T>> GetAllAsync(CancellationToken token);

        Task<T> GetAsync(CancellationToken token, params object[] key);

        Task AddAsync(CancellationToken token, T entity);

        Task RemoveAsync(CancellationToken token, params object[] key);

        Task RemoveAsync(CancellationToken token, T entity);

        Task UpdateAsync(CancellationToken token, T entity);

        Task<bool> AnyAsync(CancellationToken token, Expression<Func<T, bool>> predicate);

        IQueryable<T> FindBy(Expression<Func<T, bool>> expression);

        Task RemoveAllAsync(CancellationToken token, List<T> entity);

        IQueryable<T> Table { get; }
    }
}