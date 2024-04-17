using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.model.Commands.Queries
{
    //public class GetScoreQuery : IRequest<IReadOnlyList<EntityScoreDto>>
    public class UpdateScoreQuery : IRequest<int>
    {
        private readonly EntityScoreDto _entityScoreDto;
        public UpdateScoreQuery(EntityScoreDto entityScoreDto)
        {
            _entityScoreDto = entityScoreDto;
        }

        public EntityScoreDto GetEntity() => _entityScoreDto; 
    }
}
