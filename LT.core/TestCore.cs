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
    public class TestCore
    {

        private readonly LTContext _context;
        private readonly TestDal _dal;

        public TestCore(LTContext context)
        {
            _context = context;
            _dal = new TestDal(_context);
        }

        public List<EntityTest> Get()
        {
            return _dal.Get();
        }
        public int Insert(EntityTest entity)
        {
            return _dal.Insert(entity);
        }
    }
}
