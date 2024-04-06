using LT.dal.Access;
using LT.dal.Context;
using LT.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.core
{
    public class BaseCore <E> where E : EntityBase
    {
        private readonly LTContext _context;
        private readonly BaseDal<E> _dal;

        public BaseCore(LTContext context)
        {
            _context = context;
            _dal = new BaseDal<E>(_context);
        }

        public virtual List<E> Get()
        {
            return _dal.Get();
        }
        public virtual int Insert(E entity)
        {
            return _dal.Insert(entity);
        }
    }
}
