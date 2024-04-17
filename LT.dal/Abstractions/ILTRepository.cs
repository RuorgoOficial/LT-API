using LT.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Abstractions
{
    public interface ILTRepository<T> where T : EntityBase
    {
        Task Add(T entity);
        void Add(IEnumerable<T> entities);
        void Remove(T entity);
        void Remove(IEnumerable<T> entities);
        Task Update(T entity);
        ValueTask<T?> FindByIdAsync(long id, CancellationToken cancellationToken);
        Task<T[]> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken, bool asNoTracking = false, string? includes = null);
        Task<T[]> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
    }
}
