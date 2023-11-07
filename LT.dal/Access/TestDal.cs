using LT.dal.Context;
using LT.model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Access
{
    public class TestDal
    {
        private readonly LTContext _context;
        public TestDal(LTContext context) {
            _context = context;
        }

        public List<EntityTest> Get()
        {
            return _context.Test.ToList();
        }

        public int Insert(EntityTest test)
        {
            _context.Test.Add(test);
            _context.SaveChanges();
            return test.Id;
        }
    }
}
