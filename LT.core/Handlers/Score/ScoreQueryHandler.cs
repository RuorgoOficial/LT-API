using AutoMapper;
using Azure.Core;
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
using System.Threading;
using System.Threading.Tasks;

namespace LT.core.Handlers.Score
{
    public class ScoreQueryHandler(BaseDal<EntityScore> dal, IMapper mapper, ILTUnitOfWork unitOfWork) : 
        IRequestHandler<InsertScoreQuery, int>,
        IRequestHandler<UpdateScoreQuery, int>,
        IRequestHandler<DeleteScoreQuery, int>,
        IRequestHandler<GetScoreQuery, IEnumerable<EntityScoreDto>>
    {
        private readonly BaseDal<EntityScore> _dal = dal;
        private readonly IMapper _mapper = mapper;
        private readonly ILTUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> Handle(InsertScoreQuery request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EntityScore>(request.GetEntity());
            await _dal.Add(entity);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> Handle(UpdateScoreQuery request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EntityScore>(request.GetEntity());
            await _dal.Update(entity);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
        public async Task<int> Handle(DeleteScoreQuery request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EntityScore>(request.GetEntity());
            await _dal.Remove(entity);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
        public async Task<IEnumerable<EntityScoreDto>> Handle(GetScoreQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dal.GetAllAsync(cancellationToken);
            return entities.Select(e => _mapper.Map<EntityScoreDto>(e));
        }
        
    }
}
