using LT.dal.Context;
using LT.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Access
{
    public class BaseDal<T> where T : EntityBase
    {
        private readonly LTContext _context;
        public BaseDal(LTContext context)
        {
            _context = context;
        }

        public List<T> Get()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.ToList();
        }

        public int Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }
    }
}
