﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.model.Commands.Queries
{
    public class UpdateCommand<T> : IRequest<int>
        where T : EntityBaseDto
    {
        private readonly T _entityDto;
        public UpdateCommand(T entityDto)
        {
            _entityDto = entityDto;
        }

        public T GetEntity() => _entityDto; 
    }
}
