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
    public class ScoreCore
    {

        private readonly LTContext _context;
        private readonly ScoreDal _dal;

        public ScoreCore(LTContext context)
        {
            _context = context;
            _dal = new ScoreDal(_context);
        }

        public List<EntityScore> Get()
        {
            return _dal.Get();
        }
        public int Insert(EntityScore entity)
        {
            return _dal.Insert(entity);
        }
    }
}
