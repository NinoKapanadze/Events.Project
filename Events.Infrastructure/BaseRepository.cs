using Event.Web.Areas.Identity.Data;
using Events.Application;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Protected

        protected readonly UserContext _context;

        protected readonly DbSet<T> _dbSet;

        public IQueryable<T> Table
        {
            get
            {
                return _dbSet;
            }
        }

        #endregion Protected

        #region Ctor

        public BaseRepository(UserContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #endregion Ctor

        public async Task<List<T>> GetAllAsync(CancellationToken token)
        {
            var a = await _dbSet.ToListAsync();
            return a;
        }

        public async Task<T> GetAsync(CancellationToken token, params object[] key)
        {
            var a = await _dbSet.FindAsync(key, token);
            return a;
        }

        public async Task AddAsync(CancellationToken token, T entity)
        {
            //_context.Attach(entity);
            await _dbSet.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task SaveChanges(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }

        public async Task UpdateAsync(CancellationToken token, T entity)
        {
            if (entity == null)
                return;

            _dbSet.Update(entity);

            await _context.SaveChangesAsync(token);
        }

        public async Task RemoveAsync(CancellationToken token, params object[] key)
        {
            var entity = await GetAsync(token, key);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task RemoveAsync(CancellationToken token, T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task RemoveAllAsync(CancellationToken token, List<T> entity)
        {
            _dbSet.RemoveRange(entity);
            await _context.SaveChangesAsync(token);
        }

        public Task<bool> AnyAsync(CancellationToken token, Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate, token);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

       
    }
}