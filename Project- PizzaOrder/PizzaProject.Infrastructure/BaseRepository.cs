using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaProject.Application;
using PizzaProject.Persistemce.Context;

namespace PizzaProject.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region protected
        protected readonly PizzaProjectContext _context;

        protected readonly DbSet<T> _dbSet;
        #endregion
        public Task<int> CreateAsync(CancellationToken cancellationToken, T pizza)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(CancellationToken cancellationToken, int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CancellationToken cancellationToken, T pizza)
        {
            throw new NotImplementedException();
        }
    }
}
