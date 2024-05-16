using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.model.Commands.Queries
{
    public class GetHttpQuery<T>(int id) : IRequest<T>
        where T : EntityBaseDto
    {
        private readonly int _id = id;
        public int GetId() => _id;
    }
}
