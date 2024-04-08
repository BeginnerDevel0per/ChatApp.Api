using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetById(Guid Id);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<T> AddAsync(T entity);

        Task Update(T entity);

        Task Remove(T entity);
    }
}
