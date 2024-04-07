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
    public class ScoreCore : BaseCore<BaseDal<EntityScore>,EntityScore>
    {
        private readonly BaseDal<EntityScore> _dal;

        public ScoreCore(LTContext context, BaseDal<EntityScore> dal) : base(dal) 
        {
            _dal = dal;
        }
        
        public override List<EntityScore> Get()
        {
            return _dal.Get();
        }
        public override int Insert(EntityScore entity)
        {
            return _dal.Insert(entity);
        }
    }
}
