using LT.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Abstractions
{
    public interface IHttpRepository<T> where T : EntityBaseDto
    {
        Task<T> GetAsync(int id);
    }
}
