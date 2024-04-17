﻿using AutoMapper;
using LT.dal.Access;
using LT.dal.Context;
using LT.model;
using LT.model.Commands.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.core.Handlers.Score
{
    public class InsertScoreQueryHandler : IRequestHandler<UpdateScoreQuery, int>
    {
        private readonly BaseDal<EntityScore> _dal;
        private readonly IMapper _mapper;
        public InsertScoreQueryHandler(BaseDal<EntityScore> dal, IMapper mapper) { 
            _dal = dal;
            _mapper = mapper;
        }   
        public async Task<int> Handle(UpdateScoreQuery request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EntityScore>(request.GetEntity());
            await _dal.Add(entity);
            return entity.Id;
        }
    }
}
