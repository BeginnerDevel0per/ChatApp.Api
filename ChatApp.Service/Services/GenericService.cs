using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using ChatApp.Core.UnifOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;

        protected readonly IUnitOfWork _unitOfWork;
        public GenericService(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _repository.GetAll();
        }

        public async Task<T> GetById(Guid Id)
        {
            return await _repository.GetById(Id);
        }

        public async Task Remove(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
