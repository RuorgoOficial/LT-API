using LT.dal.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Context
{
    public class LTUnitOfWork : ILTUnitOfWork
    {
        private readonly LTDBContext _dbContext;
        public LTUnitOfWork(LTDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
