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
    public class TestDal : BaseDal<EntityTest>
    {
        private readonly LTContext _context;

        public TestDal(LTContext context) : base(context) 
        {
            _context = context;
        }

    }
}
