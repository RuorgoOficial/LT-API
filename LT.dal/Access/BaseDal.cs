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
        private readonly LTDBContext _context;
        public BaseDal(LTDBContext context)
        {
            _context = context;
        }

        public Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public void Add(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public async Task<T[]> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken, bool asNoTracking = false, string? includes = null)
        {
            var query = asNoTracking ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
            query = query.Where(spec.Criteria);

            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if(includes is not null)
                query = query.Include(includes);

            return await query.ToArrayAsync(cancellationToken);
        }

        public ValueTask<T?> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            return _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        public List<T> Get()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.ToList();
        }

        public Task<T[]> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<T> query = _context.Set<T>();
            return query.ToArrayAsync(cancellationToken);
        }

        public int Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
