using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);
        Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> ExistsAsync(int id);
        Task<bool> SaveAsync();
    }
}
