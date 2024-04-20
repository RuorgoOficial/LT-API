using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Abstractions
{
    public interface ILTUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
