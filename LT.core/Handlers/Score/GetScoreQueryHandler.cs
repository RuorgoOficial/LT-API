using AutoMapper;
using LT.dal.Abstractions;
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
    public class GetScoreQueryHandler : IRequestHandler<GetScoreQuery, IEnumerable<EntityScoreDto>>
    {
        private readonly BaseDal<EntityScore> _dal;
        private readonly IMapper _mapper;
        public GetScoreQueryHandler(BaseDal<EntityScore> dal, IMapper mapper) { 
            _dal = dal;
            _mapper = mapper;
        }   
        public async Task<IEnumerable<EntityScoreDto>> Handle(GetScoreQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dal.GetAllAsync(cancellationToken);
            return entities.Select(e => _mapper.Map<EntityScoreDto>(e));
        }
    }
}
