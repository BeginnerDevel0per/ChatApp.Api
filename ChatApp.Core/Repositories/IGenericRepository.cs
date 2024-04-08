using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public IQueryable<T> GetAll();

        public Task<T> GetById(Guid Id);

        public IQueryable<T> Where(Expression<Func<T, bool>> expression);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        
        public Task<T> AddAsync(T entity);

        public void Update(T entity);

        public void Remove(T entity);
    }
}
