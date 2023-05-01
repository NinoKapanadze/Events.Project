using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Application
{
    public interface IBaseRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetAsync(CancellationToken cancellationToken, int id);
        Task CreateAsync(CancellationToken cancellationToken, T entity);
        Task UpdateAsync(CancellationToken cancellationToken, T entity);
        Task DeleteAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
    }
}
