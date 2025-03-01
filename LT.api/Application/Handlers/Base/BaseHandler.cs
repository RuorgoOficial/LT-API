using AutoMapper;
using LT.core.RabbitMQSender;
using LT.dal.Abstractions;
using LT.dal.Context;
using LT.messageBus;
using LT.model;
using LT.model.Commands.Queries;
using MediatR;

namespace LT.api.Application.Handlers.Base
{
    public class BaseHandler<EntityT, EntityTDTO>(
        ILTRepository<EntityT> dal,
        IMapper mapper,
        ILTUnitOfWork unitOfWork,
        ILogger<BaseHandler<EntityT, EntityTDTO>> logger
    )
        :
        IRequestHandler<InsertCommand<EntityTDTO>, int>,
        IRequestHandler<UpdateCommand<EntityTDTO>, int>,
        IRequestHandler<DeleteCommand<EntityTDTO>, int>,
        IRequestHandler<GetQuery<EntityTDTO>, IEnumerable<EntityTDTO>>,
        IRequestHandler<GetQueryById<EntityTDTO>, EntityTDTO>

        where EntityT : EntityBase
        where EntityTDTO : EntityBaseDto
    {
        private readonly ILTRepository<EntityT> _dal = dal;
        private readonly IMapper _mapper = mapper;
        private readonly ILTUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<BaseHandler<EntityT, EntityTDTO>> _logger = logger;

        public async Task<int> Handle(InsertCommand<EntityTDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{nameof(InsertCommand<EntityTDTO>)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
                var entity = _mapper.Map<EntityT>(request.GetEntity());
                await _dal.Add(entity);
                return await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }
        }

        public async Task<int> Handle(UpdateCommand<EntityTDTO> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(UpdateCommand<EntityTDTO>)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var entity = _mapper.Map<EntityT>(request.GetEntity());
            await _dal.Update(entity);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Handle(DeleteCommand<EntityTDTO> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(DeleteCommand<EntityTDTO>)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var entity = _mapper.Map<EntityT>(request.GetEntity());
            await _dal.Remove(entity);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<EntityTDTO>> Handle(GetQuery<EntityTDTO> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetQuery<EntityTDTO>)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var entities = await _dal.GetAllAsync(cancellationToken);
            return entities.Select(e => _mapper.Map<EntityTDTO>(e));
        }

        public async Task<EntityTDTO> Handle(GetQueryById<EntityTDTO> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetQueryById<EntityTDTO>)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var entity = await _dal.GetByIdAsync(request.GetId(), cancellationToken);
            return _mapper.Map<EntityTDTO>(entity);
        }
    }
}
