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
    [Obsolete("This class is obsolete. You should use the handler instead.")]
    public class TestCore : BaseCore<BaseDal<EntityTest>,EntityTest>
    {

        private readonly TestDal _dal;

        public TestCore(TestDal dal) : base(dal) 
        {
            _dal = dal;
        }

        public override List<EntityTest> Get()
        {
            return _dal.Get();
        }
        public override int Insert(EntityTest entity)
        {
            return _dal.Insert(entity);
        }
    }
}
