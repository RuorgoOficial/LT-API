using LT.dal.Abstractions;
using LT.dal.Context;
using LT.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Access
{
    public class BaseDal<T> : ILTRepository<T> where T : EntityBase
    {
        private readonly DbSet<T> _dbSet;
        public BaseDal(LTDBContext context)
        {
            _dbSet = context.Set<T>();
        }

        public Task Add(T entity)
        {
            _dbSet.Add(entity);

            return Task.CompletedTask;
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task<T[]> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken, bool asNoTracking = false, string? includes = null)
        {
            var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
            query = query.Where(spec.Criteria);

            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if(includes is not null)
                query = query.Include(includes);

            return await query.ToArrayAsync(cancellationToken);
        }

        public ValueTask<T?> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public List<T> Get()
        {
            IQueryable<T> query = _dbSet;
            return query.ToList();
        }

        public Task<T[]> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;
            return query.ToArrayAsync(cancellationToken);
        }

        public int Insert(T entity)
        {
            _dbSet.Add(entity);
            return entity.Id;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public Task Update(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
    }
}
