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
    public class UpdateScoreQueryHandler : IRequestHandler<UpdateScoreQuery, int>
    {
        private readonly BaseDal<EntityScore> _dal;
        private readonly IMapper _mapper;
        private readonly ILTUnitOfWork _unitOfWork;
        public UpdateScoreQueryHandler(BaseDal<EntityScore> dal, IMapper mapper, ILTUnitOfWork unitOfWork) { 
            _dal = dal;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        public async Task<int> Handle(UpdateScoreQuery request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EntityScore>(request.GetEntity());
            await _dal.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
