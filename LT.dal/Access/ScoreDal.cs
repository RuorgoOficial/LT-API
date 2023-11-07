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
    public class ScoreDal
    {
        private readonly LTContext _context;
        public ScoreDal(LTContext context) {
            _context = context;
        }

        public List<EntityScore> Get()
        {
            return _context.Score.OrderByDescending(s => s.Score).ToList();
        }

        public int Insert(EntityScore score)
        {
            _context.Score.Add(score);
            _context.SaveChanges();
            return score.Id;
        }
    }
}
